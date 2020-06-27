using ODLSystem.BusinessLayer.PLCEntry;
using ODLSystem.Library.Common.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WATCOWebBBSR.Controllers
{
    public class PLCDataController : Controller
    {
        PLCEntryBusinessLayer pLCEntryBusinessLayer;
        public PLCDataController()
        {
            pLCEntryBusinessLayer = new PLCEntryBusinessLayer();
        }
        // GET: PLCData
        public ActionResult PLCEntry()
        {
            List<SelectListItem> items1 = new List<SelectListItem>();
            items1.Add(new SelectListItem
            {
                Text = "GBR",
                Value = "GBR"
            });
            items1.Add(new SelectListItem
            {
                Text = "ESR",
                Value = "ESR"
            });
           
            ViewBag.PilotZone = new SelectList(pLCEntryBusinessLayer.GetPilotList(), "ZoneId", "ZoneName");
            ViewBag.DistributionSource = new SelectList(items1, "Value", "Text");
            return View();
        }

        [HttpPost]
        public ActionResult PLCEntry(PLCEntityModel pLCEntityModel)
        {
            if (String.IsNullOrEmpty(pLCEntityModel.PilotZone) || String.IsNullOrEmpty(pLCEntityModel.DistributionSource) || pLCEntityModel.TotalWater == 0)
            {
                return Content("Error");
            }

            else
            {
                var result = pLCEntryBusinessLayer.insertPLCEntry(pLCEntityModel);

                if (result)
                {
                    ClearAllField(pLCEntityModel);
                    return Content("Success");
                }
            }
            return View();
        }

        private void ClearAllField(PLCEntityModel pLCEntityModel)
        {
            pLCEntityModel.PilotZone = string.Empty;
            pLCEntityModel.DistributionSource = string.Empty;
            pLCEntityModel.ReadingDate = string.Empty;
            pLCEntityModel.ESRLevel = 0;
            pLCEntityModel.FlowPressure = 0;
            pLCEntityModel.ChlorineAnalyzer = 0;
            pLCEntityModel.LastWaterFlowReading = 0;
            pLCEntityModel.TotalWater = 0;
        }

    }
}