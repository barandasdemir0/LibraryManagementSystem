using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Personal
        DBLibraryEntities entities = new DBLibraryEntities();
        public ActionResult Index(string p)
        {
            var values = entities.Personel.ToList();
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(x => x.PersonelNameSurname.Contains(p)).ToList();
            }
            return View(values);
        }


        [HttpGet]
        public ActionResult PersonelAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonelAdd(Personel Personel)
        {
            if (ModelState.IsValid)
            {
                Personel.Status = true;
                entities.Personel.Add(Personel);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("PersonelAdd");
        }

        public ActionResult PersonelDelete(int id)
        {
            var values = entities.Personel.Find(id);
            values.Status = false;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult PersonelUpdate(int id)
        {
            ViewBag.status = new List<SelectListItem>
            {
                new SelectListItem{Text = "Aktif",Value="true"},
                new SelectListItem{Text = "Pasif",Value="false"}
            };


            var values = entities.Personel.Find(id);
            return View(values);
        }

        [HttpPost]
        public ActionResult PersonelUpdate(Personel Personel)
        {
            ViewBag.status = new List<SelectListItem>
            {
                new SelectListItem{Text = "Aktif",Value="true"},
                new SelectListItem{Text = "Pasif",Value="false"}
            };
            if (!ModelState.IsValid)
            {
                return View("PersonelUpdate");
            }
            var values = entities.Personel.Find(Personel.PersonelId);
            values.PersonelNameSurname = Personel.PersonelNameSurname;
            values.Status = Personel.Status;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}