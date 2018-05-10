using Kodmine.Core.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodmine.Controllers.Base
{
    public class ControllerCRUD<T> : Controller
    {

        protected readonly IRepository<T> repository;
        protected int? EntityId;

        public ControllerCRUD(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public virtual ActionResult Index()
        {
            var posts = repository.Get();
            return View(posts);
        }

        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(T item)
        {
            try
            {
                repository.Create(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public virtual ActionResult Edit(int id)
        {
            EntityId = id;
            var model = repository.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(int id, T item)
        {
            try
            {
                repository.Update(item);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.GetBaseException().Message;
                return View("~/Views/Error/Index.cshtml");
            }
        }

        public virtual ActionResult Delete(int id)
        {
            var model = repository.GetById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int id, T collection)
        {
            //try
            //{
                repository.Delete(id);
                return RedirectToAction(nameof(Index));
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    //return View("~/Views/Error/Index.cshtml");
            //}
        }

    }
}
