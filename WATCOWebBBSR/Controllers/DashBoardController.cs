using ODLSystem.Library.Common.EntityModel;
using System;
using System.Web.Mvc;

namespace WATCOWebBBSR.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        string cityName = "";//!String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];

        string name = "Ratikant";
        public ActionResult Container()
        {
            cityName = !String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];
            ViewBag.Region = cityName;//(string)Session["Region"];
            return View();
        }
        // GET: Water Supply DashBoard
        public ActionResult WaterSupplyInfo()
        {
            ViewBag.Region = cityName;//(string)Session["Region"];
            return View();
        }
        [HttpPost]
        public ActionResult GetSummaryWaterSupply()
        {
            var cInfo = new ODLSystem.BusinessLayer.Dashboard.DashboardConsumers().GetConsumerDashboardInformation();
            return Json(cInfo);
        }
        // GET: Consumer Information
        public ActionResult ConsumerInfo()
        {
            cityName = !String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];
            ViewBag.Region = cityName;//(string)Session["Region"];
            var cInfo = new ODLSystem.BusinessLayer.Dashboard.DashboardConsumers().GetConsumerDashboardInformation();
            var serializedObj = Newtonsoft.Json.JsonConvert.SerializeObject(cInfo);
            ViewBag.CInfo = ((dynamic)Newtonsoft.Json.JsonConvert.DeserializeObject(serializedObj));
            return View();
        }
        // GET: Asset & OEM Activity
        public ActionResult AssetOEMActivity()
        {
            cityName = !String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];
            ViewBag.Region = cityName;//(string)Session["Region"];
            var aoemInfo = new ODLSystem.BusinessLayer.Dashboard.DashboardAssets().GetAssetDashboardInformation();
            var serializedObj = Newtonsoft.Json.JsonConvert.SerializeObject(aoemInfo);
            ViewBag.AOEMnfo = ((dynamic)Newtonsoft.Json.JsonConvert.DeserializeObject(serializedObj));
            return View();
        }
        // GET: User Charges Info
        public ActionResult UserChargeInfo()
        {
            cityName = !String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];
            ViewBag.Region = cityName;//(string)Session["Region"];
            var cInfo = new ODLSystem.BusinessLayer.Dashboard.DashboardConsumers().GetConsumerDashboardInformation();
            return View();
        }
        // GET: Jalsathi Performance
        public ActionResult JalSathiPerform()
        {
            cityName = !String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];
            ViewBag.Region = cityName;//(string)Session["Region"];
            var cInfo = new ODLSystem.BusinessLayer.Dashboard.DashboardConsumers().GetConsumerDashboardInformation();
            return View();
        }
        // GET: Water Quality Monitoring
        public ActionResult WaterQualityMonitor()
        {
            cityName = !String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];
            ViewBag.Region = cityName;//(string)Session["Region"];
            var cInfo = new ODLSystem.BusinessLayer.Dashboard.DashboardConsumers().GetConsumerDashboardInformation();
            return View();
        }
        // GET: Consumer Grievance
        public ActionResult ConsumerGrievance()
        {
            cityName = !String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];
            ViewBag.Region = cityName;//(string)Session["Region"];
            var cInfo = new ODLSystem.BusinessLayer.Dashboard.DashboardConsumers().GetConsumerDashboardInformation();
            return View();
        }
    }
}