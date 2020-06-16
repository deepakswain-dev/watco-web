using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODLSystem.BusinessLayer.Dashboard
{
    public class DashboardAssets
    {
        public int TotalAssetCount { get; set; }
        public int TotalPumpCount { get; set; }
        public int TotalValveCount { get; set; }
        public int TotalReservoirCount { get; set; }
        public int TotalProductionWellCount { get; set; }
        public int TotalIntakeWellCount { get; set; }
        public int TotalFlowMeterCount { get; set; }
        public int TotalStructureCount { get; set; }
        public int TotalWTPCount { get; set; }
        public int TotalPipeLength { get; set; }
        public int TotalOEMActivityToday { get; set; }
        public int TotalOEMActivityThisMonth { get; set; }
        public string TotalLengthDistributionNetwork { get; set; }
        public string TotalClearWaterNetworkLength { get; set; }

        public List<DashboardAssets> GetAssetDashboardInformation()
        {
            DataTable dt = new DatabaseHelper.DatabaseHelpersEngine().GetDashboardSummaryAsset();
            //dt = Common.Helper.TransposeTable(dt);
            List<DashboardAssets> lstADB = new List<DashboardAssets>();

            lstADB.Add(new DashboardAssets()
            {
                TotalAssetCount = Convert.ToInt32(dt.Rows[0]["TotalAsset"]),
                TotalPumpCount = Convert.ToInt32(dt.Rows[0]["TotalPump"]),
                TotalValveCount = Convert.ToInt32(dt.Rows[0]["TotalValve"]),
                TotalReservoirCount = Convert.ToInt32(dt.Rows[0]["TotalReservoir"]),
                TotalProductionWellCount = 0, //Convert.ToInt32(dt.Rows[0]["TotalProductionWell"]),
                TotalIntakeWellCount = 0, //Convert.ToInt32(dt.Rows[0]["TotalIntakeWell"]),
                TotalFlowMeterCount = Convert.ToInt32(dt.Rows[0]["TotalFlowMeter"]),
                TotalStructureCount = Convert.ToInt32(dt.Rows[0]["TotalStructure"]),
                TotalWTPCount = 0,//will be from API
                TotalPipeLength = 0,//will be from API
                TotalOEMActivityToday = 0,//will be from API
                TotalOEMActivityThisMonth = 0,//will be from API
                TotalLengthDistributionNetwork = dt.Rows[0]["TotalDistributionNetworkLength"].ToString(),
                TotalClearWaterNetworkLength = dt.Rows[0]["TotalClearWaterNetworkLength"].ToString()
            });

            return lstADB;
        }
    }
}
