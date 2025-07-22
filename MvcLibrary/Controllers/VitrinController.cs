using MvcLibrary.Models.Classes;
using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class VitrinController : Controller
    {
        // GET: Vitrin
        DBLibraryEntities DBLibraryEntities = new DBLibraryEntities();

        [HttpGet]
        public ActionResult Index()
        {
            Class1 class1 = new Class1();
            class1.Deger1 = DBLibraryEntities.Book.Take(3).ToList();
            class1.Deger2 = DBLibraryEntities.About.ToList();
            //var values = DBLibraryEntities.Book.Take(3).ToList();
            return View(class1);
        }

        [HttpPost]
        public ActionResult Index(Contact contact)
        {
            DBLibraryEntities.Contact.Add(contact);
            DBLibraryEntities.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}