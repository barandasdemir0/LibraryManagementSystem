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
    public class LoginController : Controller
    {

        DBLibraryEntities entities = new DBLibraryEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Member member)
        {
            var values = entities.Member.FirstOrDefault(x => x.MemberMail == member.MemberMail && x.Password == member.Password); // bilgiler doğrumu tut
            if (values != null)
            {
                FormsAuthentication.SetAuthCookie( values.Username, false); //form da giriş yaparsak buradan maili tut cookide kullanıcı adı yerine 
                Session["Mail"] = values.MemberMail.ToString();
                Session["Username"] = values.Username.ToString();
              
             
                return RedirectToAction("Index", "Panel");
            }
            else
            {
                return View();

            }
                
        }
    }
}