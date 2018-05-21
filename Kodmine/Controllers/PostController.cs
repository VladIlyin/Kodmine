using Kodmine.Controllers.Base;
using Kodmine.Core.Interfaces;
using Kodmine.Model.Models;
using Kodmine.ViewModel.Tag;
using Kodmine.ViewModel.Topic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kodmine.Controllers
{
    public class PostController : ControllerCRUD<Post>
    {
        private readonly IPostRepository postRepo;
        private readonly IPostTagRepository postTagRepo;
        private readonly ITagRepository tagRepo;
        private readonly IRubricRepository rubRepo;

        public PostController(IPostRepository postRepo, 
                                ITagRepository tagRepo, 
                                IPostTagRepository postTagRepo,
                                IRubricRepository rubRepo) : base(postRepo)
        {
            this.postRepo = postRepo;
            this.postTagRepo = postTagRepo;
            this.tagRepo = tagRepo;
            this.rubRepo = rubRepo;
        }

        public override ActionResult Create()
        {
            ViewBag.rubListViewModel = from t in rubRepo.Get()
                                       select new SelectListItem() { Value = t.RubricId.ToString(), Text = t.Name };
            return View();
        }

        public override ActionResult Edit(int id)
        {
            var model = repository.GetById(id);
            var tagIdList = model.PostTags.Select(x => x.TagId);

            var tagList = tagRepo.Get(); //.OrderBy(x => x.Name);
            var tagListViewModel = from t in tagList
                                       //where tagIdList.Contains(t.TagId)
                                   orderby t.Name
                                   select new TagViewModel { Id = t.TagId, Name = t.Name, Selected = tagIdList.Contains(t.TagId) };

            //ViewBag.TagIdListOnThisPost = tagListOnThisPost.Select(x => x.TagId);
            ViewBag.TagListViewModel = tagListViewModel;
            //ViewBag.TagList = tagList;

            var rubList = rubRepo.Get();
            var rubListViewModel = from t in rubList
                                       //where tagIdList.Contains(t.TagId)
                                   orderby t.Name
                                   select new TopicViewModel { Id = t.RubricId, Name = t.Name, Selected = t.RubricId == model.RubricId };

            ViewBag.RubListViewModel = rubListViewModel;

            return View(model);
        }

        public ActionResult ViewPost(int id)
        {
            var post = repository.GetById(id);
            return View(post);
        }

        public ActionResult SaveContent(int id, string content)
        {
            ((IPostRepository)repository).SaveContent(id, content);
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile images, int postId)
        {
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

        private bool ValidImageExtension(string ext)
        {
            return  ext == ".jpg" || 
                    ext == ".jpeg" || 
                    ext == ".png" || 
                    ext == ".bmp";
        }

    }
}