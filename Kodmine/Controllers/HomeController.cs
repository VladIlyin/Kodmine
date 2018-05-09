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
            var s = Newtonsoft.Json.JsonConvert.SerializeObject(new[] { 1, 2, 3 });
            var posts = postRepo.PostListMainPage(5);
            return View(posts);
        }
    }
}