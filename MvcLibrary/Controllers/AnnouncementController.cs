using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class AnnouncementController : Controller
    {
        // GET: Announcement
        DBLibraryEntities entities = new DBLibraryEntities();
        public ActionResult Index()
        {
            var result = entities.Announcement.ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult AnnouncementAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AnnouncementAdd(Announcement Announcement)
        {
            Announcement.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            entities.Announcement.Add(Announcement);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AnnouncementDelete(int id)
        {
            var values = entities.Announcement.Find(id);
            entities.Announcement.Remove(values);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AnnouncementUpdate(int id)
        {
            var values = entities.Announcement.Find(id);
          
            return View("AnnouncementUpdate", values);
        }

        [HttpPost]
        public ActionResult AnnouncementUpdate(Announcement Announcement)
        {
            Announcement.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var values = entities.Announcement.Find(Announcement.AnnouncementId);
            values.AnnouncementCategory = Announcement.AnnouncementCategory;
            values.AnnouncementContent = Announcement.AnnouncementContent;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult AnnouncementDetails(int id)
        {
            var values = entities.Announcement.Find(id);
            return View("AnnouncementDetails", values);
        }
    }
}