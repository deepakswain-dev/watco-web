using System.Data;

namespace ODLSystem.BusinessLayer.Report
{
    public class ConsumerEntity
    {
        public string GetConsumerDetailsJON(string mtrNonMtr)
        {
            DataTable dtConsumerDetails = new DatabaseHelper.DatabaseHelpersEngine().GetReportDetailsConsumer(mtrNonMtr);
            string jsonString = new Common.Helper().ConvertDataTabletoJSON(dtConsumerDetails);
            return jsonString;
        }
        public string GetConsumerBillingDetailsJSON()
        {
            DataTable dtConsumerBillingDetails = new DatabaseHelper.DatabaseHelpersEngine().GetReportDetailsConsumerBill();
            string jsonString = new Common.Helper().ConvertDataTabletoJSON(dtConsumerBillingDetails);
            return jsonString;
        }
    }
}
