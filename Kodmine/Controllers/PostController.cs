﻿using Kodmine.Controllers.Base;
using Kodmine.Core.Interfaces;
using Kodmine.Helpers;
using Kodmine.Model.Models;
using Kodmine.ViewModel.Tag;
using Kodmine.ViewModel.Topic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kodmine.Controllers
{
    [Authorize(Policy = "PostPolicy")]
    public class PostController : ControllerCRUD<Post>
    {
		private IConfiguration configuration;
        private readonly IPostRepository postRepo;
        private readonly IPostTagRepository postTagRepo;
        private readonly ITagRepository tagRepo;
        private readonly IRubricRepository rubRepo;

        public PostController(IConfiguration configuration,
								IPostRepository postRepo, 
                                ITagRepository tagRepo, 
                                IPostTagRepository postTagRepo,
                                IRubricRepository rubRepo) : base(postRepo)
        {
            this.postRepo = postRepo;
            this.postTagRepo = postTagRepo;
            this.tagRepo = tagRepo;
            this.rubRepo = rubRepo;
			this.configuration = configuration;
        }

        //public virtual ActionResult Index()
        //{
        //    return base.Index();
        //}

        public override ActionResult Create()
        {
            ViewBag.rubListViewModel = from t in rubRepo.Get()
                                       select new SelectListItem() { Value = t.RubricId.ToString(), Text = t.Name };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create(Post post)
        {
            try
            {
                repository.Create(post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public override ActionResult Edit(int id)
        {
            var model = repository.GetById(id);

            if (model == null)
            {
                ViewBag.Mesage = "Пост не найден";
                return View(@"~/Views/Error/Index.shtml");
            }

            //var regExHtmlCleaner = SettingsHelper.GetHtmlCleanerRegex(configuration);

            var tagIdList = model.PostTags.Select(x => x.TagId);

            //TODO: использовать asp-items как в Create
            var tagList = tagRepo.Get();
            var tagListViewModel = from t in tagList
                                   orderby t.Name
                                   select new TagViewModel { Id = t.TagId, Name = t.Name, Selected = tagIdList.Contains(t.TagId) };

            ViewBag.TagListViewModel = tagListViewModel;

            var rubList = rubRepo.Get();
            var rubListViewModel = from t in rubList
                                   orderby t.Name
                                   select new TopicViewModel { Id = t.RubricId, Name = t.Name, Selected = t.RubricId == model.RubricId };

            ViewBag.RubListViewModel = rubListViewModel;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit(int id, Post post)
        {
            //TODO кнопка сохранить без возврата на страницу Index
            try
            {
                repository.Update(post);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.GetBaseException().Message;
                return View(nameof(Index));
            }
        }

        public ActionResult SaveContent(int id, string content)
        {
            ((IPostRepository)repository).SaveContent(id, content);
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile images, int postId)
        {
            //TODO: в HTML оборачивать <img></img> в тег <figure> согласно HTML5
            if (images == null || images.Length == 0)
                return Content("Файл не выбран");

            if (!ValidImageExtension(Path.GetExtension(images.FileName)))
                return Content("Неподдерживаемый формат файла с изображением");

            string path = "/images_post/" + "post" + postId.ToString() + "_" + images.FileName;

            var fullPath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/" + path);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await images.CopyToAsync(stream);
            }

            return Json(path);
        }

        public IActionResult AddTag(int tagId, int postId)
        {
            postTagRepo.AddTagToPost(tagId, postId);
            return Json(true);
        }

        public IActionResult RemoveTag(int tagId, int postId)
        {
            postTagRepo.RemoveTagFromPost(tagId, postId);
            return Json(true);
        }

        public IActionResult SetTopic(int postId, int topicId)
        {
            postRepo.SetTopic(postId, topicId);
            return Json(true);
        }

        [AllowAnonymous]
        public ActionResult ViewPost(int id)
        {
            var post = repository.GetById(id);
            return View(post);
        }

        public ActionResult CleanContent(string text)
        {
            if (text == null)
               return Json(null);

            foreach (var item in SettingsHelper.GetHtmlCleanerRegex(configuration))
            {
                text = Regex.Replace(text, item.Key, item.Value);
            }

            return Json(text);
        }

        private bool ValidImageExtension(string ext)
        {
            return  ext == ".jpg" || 
                    ext == ".jpeg" || 
                    ext == ".png" || 
                    ext == ".bmp";
        }

        protected override IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(PostController.Index), "Post", new { area = "" });
            }
        }

        //[Authorize(Policy = "PostEditPolicy")]
        //public override ActionResult Delete(int id)
        //{
        //    return base.Delete(id);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Policy = "PostEditPolicy")]
        //public override ActionResult Delete(int id, Post collection)
        //{
        //    return base.Delete(id, collection);
        //}

    }
}