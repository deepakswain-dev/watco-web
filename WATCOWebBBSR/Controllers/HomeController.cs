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
        #region Testing 
        public HomeController()
        {

        }
        public HomeController(int test)
        {

        }

        public HomeController(string rr)
        {

        }

        public HomeController(int value, string strValue)
        {

        }
        #endregion
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