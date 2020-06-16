using System.Data;

namespace ODLSystem.BusinessLayer.Report
{
    public class AssetEntity
    {
        public string GetAssetDetailsJSON(string assetName)
        {
            DataTable dtConsumerAssetDetails = new DatabaseHelper.DatabaseHelpersEngine().GetReportDetailsAsset(assetName);
            string jsonString = new Common.Helper().ConvertDataTabletoJSON(dtConsumerAssetDetails);
            return jsonString;
        }
    }
}
