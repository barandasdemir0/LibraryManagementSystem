using MvcLibrary.Models.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class BookController : Controller
    {
        DBLibraryEntities entities = new DBLibraryEntities();
        // GET: Book
        public ActionResult Index(string p)
        {
            var values = entities.Book.ToList();
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(x => x.BookName.Contains(p)).ToList();
            }
            return View(values.ToList());
        }
        [HttpGet]
        public ActionResult BookAdd()
        {
            var values = entities.Category.Select(x => new SelectListItem
            {
                Value = x.CategoryId.ToString(),
                Text = x.CategoryName
            }).ToList();
            ViewBag.category = values;
            var values2 = entities.Author.Select(x => new SelectListItem
            {
                Value = x.AuthorId.ToString(),
                Text = x.AuthorName + " " + x.AuthorSurname
            }).ToList();

            ViewBag.author = values2;
            return View();
        }

        [HttpPost]
        public ActionResult BookAdd(Book book)
        {
            book.Status = true;
            entities.Book.Add(book);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BookDelete(int id)
        {
            var values = entities.Book.Find(id);
            values.Status = false;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult BookUpdate(int id)
        {
            var selectedBook = entities.Book.Find(id);

            var values = entities.Category.Select(x => new SelectListItem
            {
                Value = x.CategoryId.ToString(),
                Text = x.CategoryName
            }).ToList();

            ViewBag.category = values;

            var values2 = entities.Author.Select(x => new SelectListItem
            {
                Value = x.AuthorId.ToString(),
                Text = x.AuthorName + " " + x.AuthorSurname
            }).ToList();

            ViewBag.author = values2;

            ViewBag.status = new List<SelectListItem>
            {
                new SelectListItem{Text = "Aktif",Value="true"},
                new SelectListItem{Text = "Pasif",Value="false"}
            };

            return View(selectedBook);
        }

        [HttpPost]
        public ActionResult BookUpdate(Book book)
        {
            var values = entities.Book.Find(book.BookId);
            values.BookName = book.BookName;
            //values.Category = book.Category;
            var values2 = entities.Category.Where(x => x.CategoryId == book.Category1.CategoryId).FirstOrDefault();
            values.Category = values2.CategoryId;
            var values3 = entities.Author.Where(x => x.AuthorId == book.Author1.AuthorId).FirstOrDefault();
            values.Author = values3.AuthorId;
            values.PublicationYear = book.PublicationYear;
            values.PageNumber = book.PageNumber;
            values.Status = book.Status;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}