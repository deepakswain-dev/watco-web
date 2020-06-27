using Npgsql;
using ODLSystem.DatabaseHelper;
using ODLSystem.Library.Common.EntityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODLSystem.PersistenceLayer.Repository.PLCEntry
{
    public class PLCEntryPersistenceLayer
    {
        DatabaseHelpersEngine databaseHelpersEngine;
        public PLCEntryPersistenceLayer()
        {
            databaseHelpersEngine = new DatabaseHelpersEngine();
        }

        public bool insertPLCEntry(PLCEntityModel pLCEntityModel)
        {
            try
            {
                DbParameter[] dbParams = new DbParameter[]
            {
                new NpgsqlParameter("@_zoneid", pLCEntityModel.PilotZone),
                new NpgsqlParameter("@_distsource", pLCEntityModel.DistributionSource),
                new NpgsqlParameter("@_readingdate", pLCEntityModel.ReadingDate),
                new NpgsqlParameter("@_esrlevelmtr", pLCEntityModel.ESRLevel),
                new NpgsqlParameter("@_flowpressure", pLCEntityModel.FlowPressure),
                new NpgsqlParameter("@_chlorineanalyzerppm", pLCEntityModel.ChlorineAnalyzer),
                new NpgsqlParameter("@_lastreadingmqh", pLCEntityModel.LastWaterFlowReading),
                new NpgsqlParameter("@_totalwaterflowmq", pLCEntityModel.TotalWater),
            };

                return databaseHelpersEngine.InsertRecordInDataTable(DBConstants._1_insert_plcdata, dbParams);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<MasterPilotZone> GetPilotList()
        {
            List<MasterPilotZone> masterPilotZones = new List<MasterPilotZone>();
            try
            {
                DataTable objDataTable = GetPilotData();

                if (objDataTable != null)
                {
                    foreach (DataRow dr in objDataTable.Rows)
                    {
                        MasterPilotZone masterPilotZone = new MasterPilotZone();
                        masterPilotZone.ZoneId = (dr["ZoneId"] == System.DBNull.Value) ? (string)null : Convert.ToString(dr["ZoneId"]);
                        masterPilotZone.ZoneName = (dr["ZoneName"] == System.DBNull.Value) ? (string)null : Convert.ToString(dr["ZoneName"]);
                        masterPilotZones.Add(masterPilotZone);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return masterPilotZones;
        }

        private DataTable GetPilotData()
        {
            DataTable objDataTable = new DataTable();
            try
            {
                objDataTable = databaseHelpersEngine.GetDataTable(DBConstants._1_get_pilotzones, null);
            }
            catch (Exception)
            {
                throw;
            }

            return objDataTable;
        }
    }
}
