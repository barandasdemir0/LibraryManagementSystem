using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        DBLibraryEntities libraryEntities = new DBLibraryEntities();
        public ActionResult Index()
        {
            //sadece giriş yapan kullanıcıya gelen mesajlar
            var mail = (string)Session["Mail"];
            var mesajlar = libraryEntities.Message.Where(x=>x.Receiver==mail).ToList();
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            var senderMail = (string)Session["Mail"];//maili getirme
            message.Sender = senderMail.ToString();//gönderici mail
            message.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());//bugünün zamanı
            libraryEntities.Message.Add(message);
            libraryEntities.SaveChanges();
            return RedirectToAction("SendMessage");
        }

        public ActionResult SendMessage()
        {
            var receiverUser = (string)Session["mail"];
            var values = libraryEntities.Message.Where(x => x.Sender == receiverUser).ToList();
            return View(values);
        }
    }
}