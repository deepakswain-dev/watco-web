using System;
using System.Collections.Generic;
using System.Configuration;

namespace ODLSystem.BusinessLayer.Common
{
    public class GeoserverConfigurations
    {
        /// <summary>
        /// 0-GeoserverWMSURL
        /// 1-GeoserverWMSCapabilitiesURL
        /// </summary>
        /// <returns></returns>

        public List<string> GetGeoServerConfigurationURL()
        {
            List<string> lstGSF = new List<string>();
            lstGSF.Add(ConfigurationManager.AppSettings["GeoserverWMSURL"]);
            lstGSF.Add(ConfigurationManager.AppSettings["GeoserverWMSCapabilitiesURL"].Replace(',','&'));
            return lstGSF;
        }
    }
}
