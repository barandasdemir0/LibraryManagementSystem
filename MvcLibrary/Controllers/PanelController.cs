using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcLibrary.Controllers
{
    public class PanelController : Controller
    {
        DBLibraryEntities entities = new DBLibraryEntities();
        // GET: Panel
        [Authorize]

        [HttpGet]
        public ActionResult Index()
        {
            var values = (string)Session["Mail"];
            var detectedUser = entities.Member.FirstOrDefault(x => x.MemberMail == values);
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
    }
}