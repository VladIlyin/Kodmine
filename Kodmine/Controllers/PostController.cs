using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Kodmine.Controllers.Base;
using Kodmine.Core.Base;
using Kodmine.Core.Interfaces;
using Kodmine.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodmine.Controllers
{
    public class PostController : ControllerCRUD<Post>
    {

        public PostController(IPostRepository repository) : base(repository)
        {
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
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("Файл не выбран");

            if (!ValidImageExtension(Path.GetExtension(file.FileName)))
                return Content("Неподдерживаемый формат файла с изображением");
            
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/images_post",
                        "post" + (EntityId?.ToString() ?? "") + "_" + file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Json(true); //RedirectToAction("Files");
        }

        private bool ValidImageExtension(string ext)
        {
            return  ext == ".jpg" || 
                    ext == ".jpeg" || 
                    ext == ".png" || 
                    ext == ".bmp";
        }

        //public override ActionResult Index()
        //{
        //    var postTag = ((IPostRepository)repository).GetPostTag();
        //    return View();
        //}

    }
}