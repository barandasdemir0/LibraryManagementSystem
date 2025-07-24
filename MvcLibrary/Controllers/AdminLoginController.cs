using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcLibrary.Controllers
{
    [AllowAnonymous] // bu komut eklemezsek hiç bir sayfaya gitmez bu komut ile sadece bu sayfaya authorize işlemi yapmamızı sağlarım 
    public class AdminLoginController : Controller
    {
        DBLibraryEntities entities = new DBLibraryEntities();
        // GET: AdminLogin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Personel personel)
        {
            var bilgiler = entities.Personel.FirstOrDefault(x => x.Username == personel.Username && x.Password == personel.Password);
            if (bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Username, false);
                Session["username"] = bilgiler.Username.ToString();
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View();
            }
           
        }
    }
}