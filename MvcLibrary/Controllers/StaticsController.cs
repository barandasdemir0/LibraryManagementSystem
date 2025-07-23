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

        public ActionResult LinqCard()
        {
            var deger1 = entities.Book.Count();
            var deger2 = entities.Member.Count();
            var deger3 = entities.Punishment.Sum(x => x.PunishmentMoney);
            var deger4 = entities.Movement.Where(x => x.Status == false).Count();
            var deger5 = entities.Category.Count();
            var deger6 = entities.Movement.GroupBy(x => x.Member).Select(y => new
            {
               
                üyeadi = y.FirstOrDefault().Member1.MemberName + " " + y.FirstOrDefault().Member1.MemberSurname,
                toplam = y.Count()

            }).FirstOrDefault();


            var deger7 = entities.Movement.GroupBy(x => x.Book).Select(y => new
            {
                kitapadi = y.FirstOrDefault().Book1.BookName
            }).FirstOrDefault();

            var deger8 = entities.Book.GroupBy(x => x.Author).Select(y => new
            {
                author = y.FirstOrDefault().Author1.AuthorName+" "+y.FirstOrDefault().Author1.AuthorSurname
            }).FirstOrDefault();

            var deger10 = entities.Movement.GroupBy(x => x.Personel).Select(y => new
            {
                personelName = y.FirstOrDefault().Personel1.PersonelNameSurname
            }).FirstOrDefault();

            var deger11 = entities.Movement.GroupBy(x => x.ReturnDate).Select(y => new
            {
                bugüngelen = y.Count()
            }).FirstOrDefault();



            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            ViewBag.dgr5 = deger5;
            //ViewBag.dgr6 = deger6;
            ViewBag.üyeadi = deger6.üyeadi;
            ViewBag.toplam = deger6.toplam;
            ViewBag.dgr7 = deger7.kitapadi;
            ViewBag.dgr8 = deger8.author;
            ViewBag.dgr10 = deger10.personelName;
            ViewBag.dgr11 = deger11.bugüngelen;
            return View();
        }


    }
}