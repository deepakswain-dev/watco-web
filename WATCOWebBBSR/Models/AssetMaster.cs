using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WATCOWebBBSR.Models
{
    public class AssetMaster
    {
        public string AssetCode { get; set; }
        public string AssetName { get; set; }

        public List<AssetMaster> AssetsMaster { get; set; }
    }
}