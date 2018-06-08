using Kodmine.Core.Interfaces;
using Kodmine.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Kodmine.Controllers
{
    public class HomeController : Controller
    {

        private IPostRepository postRepo;
        private IRubricRepository rubricRepo;
        private IConfiguration configuration;
        private int take;

        public HomeController(IConfiguration configuration, IPostRepository postRepo, IRubricRepository rubricRepo)
        {
            this.postRepo = postRepo;
            this.rubricRepo = rubricRepo;
            this.configuration = configuration;
            this.take = Convert.ToInt32(configuration["postMainPageCount"]);
        }

        public IActionResult Index()
        {
            //var s = Newtonsoft.Json.JsonConvert.SerializeObject(new[] { 1, 2, 3 });
            var posts = postRepo.PostListMainPage(take);
            return View(posts);
        }

        public IActionResult PostRubric(int id)
        {
            IEnumerable<Post> posts = postRepo.GetPostByRubric(id, take);
            return View("Index", posts);
        }

    }
}