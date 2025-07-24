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
            var values = libraryEntities.Message.Where(x => x.Sender == mail).ToList();
            ViewBag.sendMessage = values.Count();
            ViewBag.totalMessage = mesajlar.Count();
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            var mail = (string)Session["Mail"];
            var mesajlar = libraryEntities.Message.Where(x => x.Receiver == mail).ToList();
            var values = libraryEntities.Message.Where(x => x.Sender == mail).ToList();
            ViewBag.sendMessage = values.Count();
            ViewBag.totalMessage = mesajlar.Count();
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
            ViewBag.sendMessage = values.Count();
            var mesajlar = libraryEntities.Message.Where(x => x.Receiver == receiverUser).ToList();
            ViewBag.totalMessage = mesajlar.Count();
            return View(values);
        }

        public ActionResult Partial1()
        {
            return PartialView();
        }


        public ActionResult MessageDetails(int id)
        {
            var receiverUser = (string)Session["mail"];
            var values = libraryEntities.Message.Where(x => x.Sender == receiverUser).ToList();
            ViewBag.sendMessage = values.Count();
            var mesajlar = libraryEntities.Message.Where(x => x.Receiver == receiverUser).ToList();
            ViewBag.totalMessage = mesajlar.Count();
            var user = libraryEntities.Message.Find(id);
            return View(user);
        }

    }
}