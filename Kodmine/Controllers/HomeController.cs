using Kodmine.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kodmine.Controllers
{
    public class HomeController : Controller
    {

        private IPostRepository postRepo;
        private IRubricRepository rubricRepo;

        public HomeController(IPostRepository postRepo, IRubricRepository rubricRepo)
        {
            this.postRepo = postRepo;
            this.rubricRepo = rubricRepo;
        }

        public IActionResult Index()
        {
            var posts = postRepo.PostListMainPage(5); //.Get().OrderByDescending(x => x.CreateDate).Take(5);
            //var rub = rubricRepo.Get();
            //ViewData["Rubric"] = rub;
            return View(posts);
        }
    }
}