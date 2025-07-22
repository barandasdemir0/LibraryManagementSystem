using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions
        DBLibraryEntities libraryEntities = new DBLibraryEntities();
        public ActionResult Index()
        {
            var values = libraryEntities.Movement.Where(x => x.Status == true).ToList();
            return View(values);
        }
    }
}