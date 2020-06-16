using ODLSystem.Library.Common.EntityModel;
using System;
using System.Web.Mvc;

namespace WATCOWebBBSR.Controllers
{
    [Authorize]
    public class MISController : Controller
    {
        string cityName = ""; //!String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];
        
        // GET: Consumer Information
        public ActionResult ConsumerInfo()
        {
            cityName = !String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];
            ViewBag.Region = cityName;//(string)Session["Region"];
            var assetMasterList = new ODLSystem.BusinessLayer.AssetDetails.AssetEntity().GetAssetList();
            var selectedList = new SelectList(assetMasterList, "AssetCode", "AssetName");
            ViewData["AllAssets"] = selectedList;
            return View();
        }
        [HttpPost]
        public ActionResult GetConsumerDetails(string mtrFilter)
        {
            try
            {
                // Display the table data.
                var jsonData = new ODLSystem.BusinessLayer.Report.ConsumerEntity().GetConsumerDetailsJON(mtrFilter);
                return Content(jsonData);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        // GET: Consumer Billing Information
        public ActionResult ConsumerBillInfo()
        {
            cityName = !String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];
            ViewBag.Region = cityName;//(string)Session["Region"];
            return View();
        }
        [HttpPost]
        public ActionResult GetConsumerBillingDetails()
        {
            try
            {
                // Display the table data.
                string jsonData = new ODLSystem.BusinessLayer.Report.ConsumerEntity().GetConsumerBillingDetailsJSON();
                return Content(jsonData);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        // GET: Asset Information
        public ActionResult AssetInfo()
        {
            cityName = !String.IsNullOrEmpty(CoreUserAuthorizationDetail.SelectedCityName) ? CoreUserAuthorizationDetail.SelectedCityName : (string)Session["Region"];
            ViewBag.Region = cityName;//(string)Session["Region"];
            var assetMasterList = new ODLSystem.BusinessLayer.AssetDetails.AssetEntity().GetAssetList();
            var selectedList = new SelectList(assetMasterList, "AssetCode", "AssetName");
            ViewData["AllAssets"] = selectedList;
            return View();
        }
        [HttpPost]
        public ActionResult GetAssetDetails(string assetName)
        {
            try
            {
                // Display the table data.
                string jsonData = new ODLSystem.BusinessLayer.Report.AssetEntity().GetAssetDetailsJSON(assetName);
                return Content(jsonData);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}