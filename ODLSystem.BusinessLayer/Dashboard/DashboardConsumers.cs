using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODLSystem.BusinessLayer.Dashboard
{
    public class DashboardConsumers
    {
        public int PilotZoneCount { get; set; }
        public int ConsumerCount { get; set; }
        public int PopulationCount { get; set; }
        public int ConsumerWithMeter { get; set; }
        public int ConsumerWithoutMeter { get; set; }
        public int NewConsumerAddedToday { get; set; }
        public int NewConsumerAddedThisMonth { get; set; }

        public List<DashboardConsumers> GetConsumerDashboardInformation()
        {
            DataTable dt = new DatabaseHelper.DatabaseHelpersEngine().GetDashboardSummaryConsumer();
            //dt = Common.Helper.TransposeTable(dt);
            //here get the consumer dahboard summary from pg query
            List<DashboardConsumers> lstCB = new List<DashboardConsumers>();

            lstCB.Add(new DashboardConsumers()
            {
                PilotZoneCount = 1,
                ConsumerCount = Convert.ToInt32(dt.Rows[0]["TotalConsumer"]),
                PopulationCount = 0,
                ConsumerWithMeter = Convert.ToInt32(dt.Rows[0]["ConsumerWithMeter"]),
                ConsumerWithoutMeter = Convert.ToInt32(dt.Rows[0]["ConsumerWithoutMeter"]),
                NewConsumerAddedToday = 0,
                NewConsumerAddedThisMonth = 0
            });

            return lstCB;
       }

    }
}
