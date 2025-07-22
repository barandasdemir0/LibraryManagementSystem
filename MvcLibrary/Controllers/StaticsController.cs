using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    
    public class StaticsController : Controller
    {

        DBLibraryEntities entities = new DBLibraryEntities();
        // GET: Statics
        public ActionResult Index()
        {
            var values = entities.Member.Count();
            var values2 = entities.Book.Count();
            var values3 = entities.Movement.Where(x=>x.Status==false).Count();
            var values4 = entities.Punishment.Sum(x => x.PunishmentMoney);
            ViewBag.v1 = values;
            ViewBag.v2 = values2;
            ViewBag.v3 = values3;
            ViewBag.v4 = values4;
            return View();
        }

        public ActionResult Weather()
        {
            return View();
        }

        public ActionResult WeatherCard()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file.ContentLength>0)
            {
                string dosyaYolu = Path.Combine(Server.MapPath("~/web2/images/"), Path.GetFileName(file.FileName));
                file.SaveAs(dosyaYolu);
            }
            return RedirectToAction("Gallery");
        }
    }
}