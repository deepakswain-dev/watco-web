using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Xml;

namespace WATCOWebBBSR.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult MapView()
        {
            List<string> geoserverURL = new ODLSystem.BusinessLayer.Common.GeoserverConfigurations().GetGeoServerConfigurationURL();
            ViewBag.GeoserverURL = geoserverURL[0];
            ViewBag.WMSCapabilitiesURL = geoserverURL[1];
            var assetMasterList = new ODLSystem.BusinessLayer.AssetDetails.AssetEntity().GetAssetList();
            var selectedList = new SelectList(assetMasterList, "AssetCode", "AssetName");
            ViewData["AllAssets"] = selectedList;
            return View();
        }
        [HttpPost]
        public ActionResult GetMapTOC()
        {
            string tocXML = Server.MapPath("~/App_Data/LayerConfig.xml");
            XmlDocument dom = new XmlDocument();
            dom.Load(tocXML);
            XmlNode inXmlNode = dom.DocumentElement;
            XmlNode xNode;
            XmlNodeList nodeList;
            return Content("");
        }

        [HttpPost]
        public ActionResult GetFeatureInfo(string URLString)
        {
            try
            {
                // Create a new WebClient instance.
                WebClient myWebClient = new WebClient();
                // Download the Web resource and save it into a data buffer.
                byte[] myDataBuffer = myWebClient.DownloadData(URLString);
                // Display the downloaded data.
                string download = Encoding.ASCII.GetString(myDataBuffer);
                return Content(download);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult GetAssetDetails(string AssetID, string Buffer, string ClickedLat, string ClickedLng)
        {
            try
            {
                // Display the table data.
                string jsonData = new ODLSystem.BusinessLayer.AssetDetails.AssetEntity().GetAssetJSON(AssetID, Buffer, ClickedLat, ClickedLng);
                return Content(jsonData);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult GetAssetDetailsQuery(string AssetID, string AssetColumn, string AssetCriteria, string AssetValue)
        {
            try
            {
                // Display the table data.
                string jsonData = new ODLSystem.BusinessLayer.AssetDetails.AssetEntity().GetAssetJSONQuery(AssetID, AssetColumn, AssetCriteria, AssetValue);
                return Content(jsonData);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}