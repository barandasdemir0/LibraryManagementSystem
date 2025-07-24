using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    [AllowAnonymous] // bu komut eklemezsek hiç bir sayfaya gitmez bu komut ile sadece bu sayfaya authorize işlemi yapmamızı sağlarım 
    public class RegisterController : Controller
    {
        // GET: Register
        DBLibraryEntities entities = new DBLibraryEntities();
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Member member)
        {
            member.Status = true;
            entities.Member.Add(member);
            entities.SaveChanges();
            return RedirectToAction("Login", "Login");

        }
    }
}