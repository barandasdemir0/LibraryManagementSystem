using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{

    public class CategoryController : Controller
    {
        // GET: Category
        DBLibraryEntities db = new DBLibraryEntities();
        public ActionResult Index()
        {
            var values = db.Category.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdd(Category category)
        {
            category.Status = true;
            db.Category.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CategoryDelete(int id)
        {
            var values = db.Category.Find(id);
            values.Status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CategoryUpdate(int id)
        {
            var values = db.Category.Find(id);
            ViewBag.status = new List< SelectListItem>
            {
                new SelectListItem{Text="Aktif" ,Value="true"},
                new SelectListItem{Text="Pasif",Value="false"}
            };
            return View("CategoryUpdate", values);
        }

        [HttpPost]
        public ActionResult CategoryUpdate(Category category)
        {
            var values = db.Category.Find(category.CategoryId);
            values.CategoryName = category.CategoryName;
            values.Status = category.Status;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}