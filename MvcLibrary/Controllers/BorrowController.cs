using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class BorrowController : Controller
    {
        DBLibraryEntities dB = new DBLibraryEntities();
        // GET: Borrow
        public ActionResult Index(string p)
        {
            var values = dB.Movement.Where(x=>x.Status==false).ToList();
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(x => x.Member1.MemberName.Contains(p) || x.Member1.MemberSurname.Contains(p)).ToList();
            }
            return View(values);



        }

        [HttpGet]
        public ActionResult GiveBorrow()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GiveBorrow(Movement movement)
        {
            movement.Status = false;
            dB.Movement.Add(movement);
            dB.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult TakeBorrow()
        {
            var values = dB.Movement.Where(x => x.Status == false).ToList();
            return View(values);
        }

        public ActionResult ReturnBook(int id)
        {
            var values = dB.Movement.Find(id);
            DateTime d1 = DateTime.Parse(values.ReturnDate.ToString());
            DateTime d2 = DateTime.Parse(values.ReturnMembersDate.ToString());
            TimeSpan fark = d2 - d1;
            ViewBag.fark = fark.TotalDays;
            return View("ReturnBook", values);
        }

        public ActionResult BorrowUpdate(Movement movement)
        {
            var values = dB.Movement.Find(movement.MovementId);
            values.ReturnMembersDate = movement.ReturnMembersDate;
            values.Status = true;
            dB.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BorrowDetail(int id)
        {
            var values = dB.Movement.Where(x => x.Member == id).ToList();
            var user = dB.Member.Where(x => x.MemberId == id).FirstOrDefault();
            ViewBag.name = user.MemberName + " " + user.MemberSurname;
            return View(values);
        }
    }
}