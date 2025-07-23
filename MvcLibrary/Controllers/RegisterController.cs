using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
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