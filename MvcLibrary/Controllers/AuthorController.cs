using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class AuthorController : Controller
    {
        DBLibraryEntities dB = new DBLibraryEntities();
        // GET: Author
        public ActionResult Index()
        {
            var values = dB.Author.ToList();
            return View(values);
        }


        [HttpGet]
        public ActionResult AuthorAdd()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AuthorAdd(Author author)
        {

            author.Status = true;
            dB.Author.Add(author);
            dB.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult AuthorDelete(int id)
        {
            var values = dB.Author.Find(id);
            values.Status = false;
            dB.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult AuthorUpdate(int id)
        {
            ViewBag.status = new List<SelectListItem>
            {
                new SelectListItem{Text = "Aktif",Value="true"},
                new SelectListItem{Text = "Pasif",Value="false"}
            };


            var values = dB.Author.Find(id);
            return View(values);
        }

        [HttpPost]
        public ActionResult AuthorUpdate(Author author)
        {
            var values = dB.Author.Find(author.AuthorId);
            values.AuthorName = author.AuthorName;
            values.AuthorSurname = author.AuthorSurname;
            values.AuthorDetails = author.AuthorDetails;
            values.Status = author.Status;
            dB.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AuthorDetails(int id)
        {
          
            var values = dB.Book.Where(x=>x.Author == id).ToList();
            //diyorumkii author yani benim kitap tablosundaki yazarım ile gelen id eşleşen sonuçları getir
            var yazarname = dB.Author.Where(x => x.AuthorId == id).Select(y => y.AuthorName + " " + y.AuthorSurname).FirstOrDefault();
            ViewBag.name = yazarname;
            return View(values);
        }

    }
}