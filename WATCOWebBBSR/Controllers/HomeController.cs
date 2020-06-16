using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WATCOWebBBSR.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
                
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DashboardV1()
        {
            return View();
        }
        public ActionResult DashboardV2()
        {
            return View();
        }
    }
}