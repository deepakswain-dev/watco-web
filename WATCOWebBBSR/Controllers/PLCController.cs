using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WATCOWebBBSR.Models;

namespace WATCOWebBBSR.Controllers
{
    public class PLCController : Controller
    {
        // GET: PLC
        public ActionResult PLCEntry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PLCEntry(PLCEntityModel pLCEntityModel)
        {

            if (ModelState.IsValid)
            {

            }
            return View();
        }
    }
}