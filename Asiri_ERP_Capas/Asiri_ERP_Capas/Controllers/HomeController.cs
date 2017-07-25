using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Microsoft.AspNet.Identity;

namespace Asiri_ERP_Capas.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private AsiriContext db = new AsiriContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult LoadMenu()
        {
            var user = User.Identity.GetUserId();
            var menu = db.MenuTemp.Where(x => x.UserID == user);
            return View(menu.ToList());
        }



    }
}