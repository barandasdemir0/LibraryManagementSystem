using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcLibrary.Controllers
{
    [Authorize] // bu controller ve index tarafında authorize işlemi yapar
    public class PanelController : Controller
    {
        DBLibraryEntities entities = new DBLibraryEntities();
        // GET: Panel
       

        [HttpGet]
        public ActionResult Index()
        {
            var values = (string)Session["Mail"];
            var detectedUser = entities.Member.FirstOrDefault(x => x.MemberMail == values);
  
            var userName = entities.Member.Where(x => x.MemberMail == values).Select(y => y.MemberName + " "+y.MemberSurname).FirstOrDefault();
            ViewBag.name = userName;
            var school = entities.Member.Where(x => x.MemberMail == values).Select(y => y.School).FirstOrDefault();
            ViewBag.school = school;
            var username = entities.Member.Where(x => x.MemberMail == values).Select(y => y.Username).FirstOrDefault();
            ViewBag.username = username;
            var telephone = entities.Member.Where(x => x.MemberMail == values).Select(y => y.Telephone).FirstOrDefault();
            ViewBag.telephone = telephone;
            var mail = entities.Member.Where(x => x.MemberMail == values).Select(y=>y.MemberMail).FirstOrDefault();
            ViewBag.mail = mail;

            var User = entities.Member.Where(x => x.MemberMail == values).Select(x => x.MemberId).FirstOrDefault();
            var totalBook = entities.Movement.Where(x => x.Member == User).Count();
            ViewBag.userTotalBook = totalBook;

            var bookName = entities.Movement.Where(z=>z.Member==User).OrderByDescending(x => x.MovementId).Select(y => y.Book1.BookName).ToList();
            ViewBag.userLastBook = bookName.FirstOrDefault();


            var messageList = entities.Message.Where(x => x.Receiver == mail).Count();
            ViewBag.listMessage = messageList;



            return View(detectedUser);
        }
        [HttpPost]
        public ActionResult Index2(Member member)
        {
            var kullanici = (string)Session["Mail"]; //açılan kullanıcı kimdi gibi mantık var burada
            var values = entities.Member.FirstOrDefault(x => x.MemberMail == kullanici);
            values.Password = member.Password;
            values.Telephone = member.Telephone;
            entities.SaveChanges();
            return View("Index", values);
        }

        public ActionResult MyBook()
        {
            var user = (string)Session["Mail"];
            var kullanici = entities.Member.Where(x => x.MemberMail == user.ToString()).Select(y=>y.MemberId).FirstOrDefault();
            var book = entities.Movement.Where(x => x.Member == kullanici).ToList();
            return View(book);
        }


        public ActionResult Announcement()
        {
            var values = entities.Announcement.ToList();
            return View(values);
        }

        public ActionResult AnnouncementDetails(int id)
        {
            var values = entities.Announcement.Find(id);
            return View(values);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }


        public PartialViewResult partial1()
        {
            var value = entities.Announcement.ToList();
            return PartialView("partial1",value);
        }
    }
}