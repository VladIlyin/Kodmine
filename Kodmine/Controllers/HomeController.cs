using Kodmine.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Kodmine.Controllers
{
    public class HomeController : Controller
    {

        private IPostRepository postRepo;
        private IRubricRepository rubricRepo;
        private IConfiguration configuration;

        public HomeController(IConfiguration configuration, IPostRepository postRepo, IRubricRepository rubricRepo)
        {
            this.postRepo = postRepo;
            this.rubricRepo = rubricRepo;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            //var s = Newtonsoft.Json.JsonConvert.SerializeObject(new[] { 1, 2, 3 });
            var posts = postRepo.PostListMainPage(Convert.ToInt32(configuration["postMainPageCount"]));
            return View(posts);
        }
    }
}