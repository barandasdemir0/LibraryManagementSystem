using MvcLibrary.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class MembersController : Controller
    {
        DBLibraryEntities entities = new DBLibraryEntities();
        public ActionResult Index(int sayfa=1,string p="")
        {
            var query = entities.Member.AsQueryable();
            
            if (!string.IsNullOrEmpty(p))
            {
                query = query.Where(x => x.MemberName.Contains(p) || x.Username.Contains(p) || x.MemberSurname.Contains(p));
            }

            var values =query.OrderBy(x=>x.MemberId).ToPagedList(sayfa, 5);
            return View(values);
        }


        [HttpGet]
        public ActionResult MemberAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MemberAdd(Member member)
        {

                member.Status = true;
                entities.Member.Add(member);
                entities.SaveChanges();
                return RedirectToAction("Index");
            
        }

        public ActionResult MemberDelete(int id)
        {
            var values = entities.Member.Find(id);
            values.Status = false;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MemberUpdate(int id)
        {
            ViewBag.status = new List<SelectListItem>
            {
                new SelectListItem{Text = "Aktif",Value="true"},
                new SelectListItem{Text = "Pasif",Value="false"}
            };


            var values = entities.Member.Find(id);
            return View(values);
        }

        [HttpPost]
        public ActionResult MemberUpdate(Member member)
        {
            ViewBag.status = new List<SelectListItem>
            {
                new SelectListItem{Text = "Aktif",Value="true"},
                new SelectListItem{Text = "Pasif",Value="false"}
            };
           
            var values = entities.Member.Find(member.MemberId);
            values.MemberName = member.MemberName;
            values.MemberSurname = member.MemberSurname;
            values.MemberMail = member.MemberMail;
            values.Username = member.Username;
            values.Password = member.Password;
            values.Image = member.Image;
            values.Telephone = member.Telephone;
            values.School = member.School;
            values.Status = member.Status;
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}