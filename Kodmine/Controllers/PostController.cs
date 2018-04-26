using System;
using System.Collections.Generic;
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

        //public override ActionResult Index()
        //{
        //    var postTag = ((IPostRepository)repository).GetPostTag();
        //    return View();
        //}

    }
}