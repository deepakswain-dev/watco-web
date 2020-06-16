using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODLSystem.BusinessLayer.AssetDetails
{
    public class AssetEntity
    {
        public string AssetCode { get; set; }
        public string AssetName { get; set; }

        public List<AssetEntity> GetAssetList()
        {
            DataTable dtAssetList = new DatabaseHelper.DatabaseHelpersEngine().GetAssetListFromAssetMaster();
            List<AssetEntity> lstAsset= dtAssetList.AsEnumerable()
                                        .Select(dr => new AssetEntity()
                                        {
                                            AssetCode = dr.Field<string>("AssetCode"),
                                            AssetName = dr.Field<string>("AssetName")
                                        })
                                        .ToList();
            return lstAsset;
        }

        public string GetAssetJSON(string assetId, string buffer, string clickedLat, string clickedLng)
        {
            DataTable dtAssetDetails = new DatabaseHelper.DatabaseHelpersEngine().GetAssetMap(assetId, buffer, clickedLat, clickedLng);
            string jsonString = new Common.Helper().ConvertDataTabletoJSON(dtAssetDetails);
            return jsonString;
        }
        public string GetAssetJSONQuery(string assetId, string assetColumn, string assetCriteria, string assetValue)
        {
            DataTable dtAssetDetails = new DatabaseHelper.DatabaseHelpersEngine().GetAssetQuery(assetId, assetColumn, assetCriteria, assetValue);
            string jsonString = new Common.Helper().ConvertDataTabletoJSON(dtAssetDetails);
            return jsonString;
        }
    }
}
