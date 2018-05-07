using Kodmine.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kodmine.Controllers
{
    public class HomeController : Controller
    {

        private IPostRepository repository;

        public HomeController(IPostRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var posts = repository.PostListMainPage(5); //.Get().OrderByDescending(x => x.CreateDate).Take(5);
            return View(posts);
        }
    }
}