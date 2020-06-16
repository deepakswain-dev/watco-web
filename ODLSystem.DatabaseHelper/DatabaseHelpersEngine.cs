#region Copyright
/******************************************************************************
* Copyright (C) 2018-2022 MicroGlobalTranz - All Rights Reserved.
*
* Proprietary and confidential. Unauthorized copying of this file, via any
* medium is strictly prohibited without the explicit permission of MicroGlobalTranz.
* Powered By Deepak Swain
******************************************************************************/
#endregion Copyright

using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODLSystem.DatabaseHelper
{
    /// <summary>
    /// DatabaseHelpersEngine
    /// </summary>
    public class DatabaseHelpersEngine
    {
        private readonly string ODLConnectionString = ConfigurationManager.ConnectionStrings["ODLConnection"].ConnectionString;
        private readonly string ODLConnectionStringPG = ConfigurationManager.ConnectionStrings["ODLConnectionPG"].ConnectionString;
        /// <summary>
        /// DeleteFromDataTable
        /// </summary>
        /// <param name="prcedureName">prcedureName</param>
        /// <param name="dbParams">dbParams</param>
        /// <returns></returns>
        public bool DeleteFromDataTable(string prcedureName, DbParameter[] dbParams)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(ODLConnectionString))
                {
                    if (objSqlConnection.State != ConnectionState.Open)
                    {
                        objSqlConnection.Open();
                    }

                    using (SqlCommand objSqlCommand = new SqlCommand(prcedureName, objSqlConnection))
                    {
                        objSqlCommand.CommandType = CommandType.StoredProcedure;

                        if (dbParams != null)
                        {
                            objSqlCommand.Parameters.AddRange(dbParams);
                        }

                        objSqlCommand.ExecuteNonQuery();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        /// <summary>
        /// GetDataTable
        /// </summary>
        /// <param name="prcedureName">prcedureName</param>
        /// <param name="dbParams">dbParams</param>
        /// <returns></returns>
        public DataTable GetDataTable(string prcedureName, DbParameter[] dbParams)
        {
            DataTable objDataTable = new DataTable();
            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(ODLConnectionString))
                {
                    if (objSqlConnection.State != ConnectionState.Open) objSqlConnection.Open();

                    using (SqlCommand objSqlCommand = new SqlCommand(prcedureName, objSqlConnection))
                    {
                        objSqlCommand.CommandType = CommandType.StoredProcedure;
                        if (dbParams != null)
                        {
                            objSqlCommand.Parameters.AddRange(dbParams);
                        }

                        using (SqlDataReader ndr = objSqlCommand.ExecuteReader(CommandBehavior.Default))
                        {
                            if (ndr.HasRows)
                            {
                                objDataTable.Load(ndr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objDataTable;
        }

        /// <summary>
        /// GetDataTableById
        /// </summary>
        /// <param name="prcedureName">prcedureName</param>
        /// <param name="dbParams">dbParams</param>
        /// <returns></returns>
        public DataTable GetDataTableById(string prcedureName, DbParameter[] dbParams)
        {
            DataTable objDataTable = new DataTable();
            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(ODLConnectionString))
                {
                    if (objSqlConnection.State != ConnectionState.Open) objSqlConnection.Open();

                    using (SqlCommand objSqlCommand = new SqlCommand(prcedureName, objSqlConnection))
                    {
                        objSqlCommand.CommandType = CommandType.StoredProcedure;
                        if (dbParams != null)
                        {
                            objSqlCommand.Parameters.AddRange(dbParams);
                        }

                        using (SqlDataReader ndr = objSqlCommand.ExecuteReader(CommandBehavior.Default))
                        {
                            if (ndr.HasRows)
                            {
                                objDataTable.Load(ndr);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return objDataTable;
        }

        /// <summary>
        /// GetIdFromTable
        /// </summary>
        /// <param name="prcedureName">prcedureName</param>
        /// <param name="dbParams">dbParams</param>
        /// <returns></returns>
        public int GetIdFromTable(string prcedureName, DbParameter[] dbParams)
        {
            int returnString = 0;
            try
            {
                using (SqlConnection ncon = new SqlConnection(ODLConnectionString))
                {
                    if (ncon.State != ConnectionState.Open)
                    {
                        ncon.Open();
                    }

                    using (SqlCommand objSqlCommand = new SqlCommand(prcedureName, ncon))
                    {

                        objSqlCommand.CommandType = CommandType.StoredProcedure;
                        if (dbParams != null)
                        {
                            objSqlCommand.Parameters.AddRange(dbParams);
                        }
                        using (SqlDataReader ndr = objSqlCommand.ExecuteReader(CommandBehavior.Default))
                        {
                            DataTable objDataTable = new DataTable();
                            if (ndr.HasRows)
                            {
                                objDataTable.Load(ndr);
                                returnString = Convert.ToInt32(objDataTable.Rows[0]["MyColumnNAme"]);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return returnString;
        }

        public string GetScalarValueFromTable(string prcedureName, DbParameter[] dbParams)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// InsertRecordInDataTable
        /// </summary>
        /// <param name="prcedureName">prcedureName</param>
        /// <param name="dbParams">dbParams</param>
        /// <returns></returns>
        public bool InsertRecordInDataTable(string prcedureName, DbParameter[] dbParams)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(ODLConnectionString))
                {
                    if (objSqlConnection.State != ConnectionState.Open)
                    {
                        objSqlConnection.Open();
                    }

                    using (SqlCommand objSqlCommand = new SqlCommand(prcedureName, objSqlConnection))
                    {
                        objSqlCommand.CommandType = CommandType.StoredProcedure;
                        if (dbParams != null)
                        {
                            objSqlCommand.Parameters.AddRange(dbParams);
                        }
                        objSqlCommand.ExecuteNonQuery();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        /// <summary>
        /// UpdateRecordInDataTable
        /// </summary>
        /// <param name="prcedureName">prcedureName</param>
        /// <param name="dbParams">dbParams</param>
        /// <returns></returns>
        public bool UpdateRecordInDataTable(string prcedureName, DbParameter[] dbParams)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(ODLConnectionString))
                {
                    if (objSqlConnection.State != ConnectionState.Open)
                    {
                        objSqlConnection.Open();
                    }

                    using (SqlCommand objSqlCommand = new SqlCommand(prcedureName, objSqlConnection))
                    {
                        objSqlCommand.CommandType = CommandType.StoredProcedure;
                        if (dbParams != null)
                        {
                            objSqlCommand.Parameters.AddRange(dbParams);
                        }
                        objSqlCommand.ExecuteNonQuery();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }


        /// <summary>
        /// ValidateUserCredential
        /// </summary>
        /// <param name="prcedureName">prcedureName</param>
        /// <param name="dbParams">dbParams</param>
        /// <returns></returns>
        public bool ValidateUserCredential(string prcedureName, DbParameter[] dbParams)
        {
            bool isSuccess = false;
            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(ODLConnectionString))
                {
                    if (objSqlConnection.State != ConnectionState.Open)
                    {
                        objSqlConnection.Open();
                    }

                    using (SqlCommand objSqlCommand = new SqlCommand(prcedureName, objSqlConnection))
                    {
                        objSqlCommand.CommandType = CommandType.StoredProcedure;

                        if (dbParams != null)
                        {
                            objSqlCommand.Parameters.AddRange(dbParams);
                        }

                        if (objSqlCommand.ExecuteScalar() == null || (int)objSqlCommand.ExecuteScalar() > 0)
                        {
                            isSuccess = false;
                        }
                        else
                        {
                            isSuccess = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return isSuccess;
        }


        #region PostgreSQL
        
        public DataTable GetUserDetails(string userName,string password)
        {
            string pgQuery= "SELECT UserId, U.UserName,U.UserPassword,U.UserFullName,C.CityId,C.CityName,U.UserEmailId,U.UserPhone FROM UserDetails U "+
                            "INNER JOIN City C ON C.CityId = U.CityId WHERE U.UserName = '"+userName+"' AND U.UserPassword = '"+password+"'";
            DataTable dt = Common.CommonHelpers.GetDataTablePGQuery(pgQuery);
            return dt;
        }
        /// <summary>
        /// fetch all the assets from asset master
        /// </summary>
        /// <returns></returns>
        public DataTable GetAssetListFromAssetMaster()
        {
            DataTable dt = Common.CommonHelpers.GetDataTablePGFunction("_1_get_assetlist", null);
            return dt;
        }

        public DataTable GetAssetMap(string assetId, string buffer, string clickedLat, string clickedLng)
        {
            Int32 bufferRadius = Convert.ToInt32(buffer);
            double latitude = Convert.ToDouble(clickedLat);
            double longitude = Convert.ToDouble(clickedLng);
            //string qryString = ""; assetId = "bbsr_" + assetId;
            string spName = "";
            switch (assetId)
            {
                case "S21":  //Sewers
                    spName = "_1_get_asset";
                    break;
                case "S22":  //Manholes
                    spName = "_1_get_asset";
                    break;
                case "S23":  //Sewerage Treatment Facilities
                    spName = "_1_get_asset";
                    break;
                case "S24":  //Sewerage Pumphouse
                    spName = "_1_get_asset";
                    break;
                case "S25":  //Sewerage Pump
                    spName = "_1_get_asset";
                    break;
                case "S26":  //Discharge Point
                    spName = "_1_get_asset";
                    break;
                case "W01":  //Intake Well
                    spName = "_1_get_asset";
                    break;
                case "W02":  //Water Treatment Plant
                    spName = "_1_get_asset";
                    break;
                case "W04":  //Pump
                    spName = "_1_get_assetquerydetails_pump";
                    break;
                case "W05":  //Pipe
                    spName = "_1_get_asset";
                    break;
                case "W06":  //Valve
                    spName = "_1_get_assetquerydetails_valve";
                    break;
                case "W07":  //Production Well
                    spName = "_1_get_asset";
                    break;
                case "W40":  //Reservoir
                    spName = "_1_get_assetquerydetails_reservoir";
                    break;
                case "W41":  //Meter
                    spName = "_1_get_assetquerydetails_flowmeter";
                    break;
                case "WH4":  //Pump House
                    spName = "_1_get_asset";
                    break;

                default:
                    break;
            }
            DbParameter[] dbParams = new DbParameter[] {
                new NpgsqlParameter("@assetcode",assetId),
                new NpgsqlParameter("@clickedlng",longitude),
                new NpgsqlParameter("@clickedlat",latitude),
                new NpgsqlParameter("@bufferradius",bufferRadius)
            };
            DataTable dtResult = Common.CommonHelpers.GetDataTablePGFunction(spName, dbParams);
            return dtResult;
        }

        public DataTable GetAssetQuery(string assetId, string assetColumn, string assetCriteria, string assetValue)
        {
            string qryString = ""; assetId = "bbsr_" + assetId;
            Int32 num; double num1;string assetQuery = "";
            if (int.TryParse(assetValue, out num))
            {
                //build query
                assetQuery = assetColumn + assetCriteria + "'" + assetValue + "'";
            }
            else if (double.TryParse(assetValue, out num1))
            {
                //build query
                assetQuery = assetColumn + assetCriteria + assetValue;
            }
            else
            {
                //asset value is string
                assetQuery = assetColumn + assetCriteria + "'"+ assetValue+"'";
            }
            switch (assetId)
            {
                case "bbsr_connection":
                    qryString = "SELECT type,length,TO_CHAR(ST_X(geom), '99D999999') as Longitude,TO_CHAR(ST_Y(geom), '99D999999') as Latitude  from " + assetId + " " +
                    "where "+ assetQuery;
                    break;
                case "bbsr_consumer":
                    qryString = "SELECT division,sub_div,section,node_s_1,node_e_1,l_r_no,ws_y_n,name_own,TO_CHAR(ST_X(geom), '99D999999') as Longitude,TO_CHAR(ST_Y(geom), '99D999999') as Latitude  from " + assetId + " " +
                    "where " + assetQuery;
                    break;
                case "bbsr_flow_meter":
                    qryString = "SELECT division,sub_div,section,make,yr_instl,man_dig,supply_to,TO_CHAR(ST_X(geom), '99D999999') as Longitude,TO_CHAR(ST_Y(geom), '99D999999') as Latitude  from " + assetId + " " +
                    "where " + assetQuery;
                    break;
                case "bbsr_instrument":
                    qryString = "SELECT division,sub_div,section,type,make,manu_auto,yr_instl,TO_CHAR(ST_X(geom), '99D999999') as Longitude,TO_CHAR(ST_Y(geom), '99D999999') as Latitude  from " + assetId + " " +
                    "where " + assetQuery;
                    break;
                case "bbsr_node":
                    qryString = "SELECT number,TO_CHAR(ST_X(geom), '99D999999') as Longitude,TO_CHAR(ST_Y(geom), '99D999999') as Latitude  from " + assetId + " " +
                    "where " + assetQuery;
                    break;
                case "bbsr_pump":
                    qryString = "SELECT division,sub_div,section,type,make,manu_auto,efficiency,yr_instl,TO_CHAR(ST_X(geom), '99D999999') as Longitude,TO_CHAR(ST_Y(geom), '99D999999') as Latitude  from " + assetId + " " +
                    "where " + assetQuery;
                    break;
                case "bbsr_reservoir":
                    qryString = "SELECT section,sub_div,division,location,type,material,yr_const,stage_ht,capacity,unit,cap_ltr,shp,inflow_dia,TO_CHAR(ST_X(geom), '99D999999') as Longitude,TO_CHAR(ST_Y(geom), '99D999999') as Latitude  from " + assetId + " " +
                    "where " + assetQuery;
                    break;
                case "bbsr_structure":
                    qryString = "SELECT type,TO_CHAR(ST_X(geom), '99D999999') as Longitude,TO_CHAR(ST_Y(geom), '99D999999') as Latitude  from " + assetId + " " +
                    "where " + assetQuery;
                    break;
                case "bbsr_valve":
                    qryString = "SELECT division,sub_div,section,name,type,make,material,dia_size,yr_instl,status_i_t,man_auto,flow_mld,TO_CHAR(ST_X(geom), '99D999999') as Longitude,TO_CHAR(ST_Y(geom), '99D999999') as Latitude  from " + assetId + " " +
                    "where " + assetQuery;
                    break;
                default:
                    break;
            }
            DataTable dt = Common.CommonHelpers.GetDataTablePGQuery(qryString);
            return dt;
        }

        public DataTable GetReportDetailsConsumer(string mtrNonMtr)
        {
            DbParameter[] dbParams = new DbParameter[] {
                new NpgsqlParameter("@mtrnonmtr",mtrNonMtr)
            };
            DataTable dtResult = Common.CommonHelpers.GetDataTablePGFunction("_1_get_misreportdetails_consumer", dbParams);
            return dtResult;
        }

        public DataTable GetReportDetailsConsumerBill()
        {
            DataTable dtResult = Common.CommonHelpers.GetDataTablePGFunction("_1_get_misreportdetails_consumerbill", null);
            return dtResult;
        }

        public DataTable GetReportDetailsAsset(string assetId)
        {
            string spName = "";
            switch (assetId)
            {
                case "S21":  //Sewers
                    spName = "_1_get_asset";
                    break;
                case "S22":  //Manholes
                    spName = "_1_get_asset";
                    break;
                case "S23":  //Sewerage Treatment Facilities
                    spName = "_1_get_asset";
                    break;
                case "S24":  //Sewerage Pumphouse
                    spName = "_1_get_asset";
                    break;
                case "S25":  //Sewerage Pump
                    spName = "_1_get_asset";
                    break;
                case "S26":  //Discharge Point
                    spName = "_1_get_asset";
                    break;
                case "W01":  //Intake Well
                    spName = "_1_get_asset";
                    break;
                case "W02":  //Water Treatment Plant
                    spName = "_1_get_asset";
                    break;
                case "W04":  //Pump
                    spName = "_1_get_misreportdetails_asset_pump";
                    break;
                case "W05":  //Pipe
                    spName = "_1_get_asset";
                    break;
                case "W06":  //Valve
                    spName = "_1_get_misreportdetails_asset_valve";
                    break;
                case "W07":  //Production Well
                    spName = "_1_get_asset";
                    break;
                case "W40":  //Reservoir
                    spName = "_1_get_misreportdetails_asset_reservoir";
                    break;
                case "W41":  //Meter
                    spName = "_1_get_misreportdetails_asset_flowmeter";
                    break;
                case "WH4":  //Pump House
                    spName = "_1_get_asset";
                    break;

                default:
                    break;
            }
            DbParameter[] dbParams = new DbParameter[] {
                new NpgsqlParameter("@_assetcode",assetId)
            };
            DataTable dtResult = Common.CommonHelpers.GetDataTablePGFunction(spName, dbParams);
            return dtResult;
        }

        #region dashboard
        public DataTable GetDashboardSummaryConsumer()
        {
            //DbParameter[] parameters = new DbParameter[]
            //{
            //    Common.CommonHelpers.CreateParameter("@FromDate", fromDate),
            //    Common.CommonHelpers.CreateParameter("@ToDate", toDate)
            //};
            DataTable dtResult = Common.CommonHelpers.GetDataTablePGFunction("_1_get_dashboardsummary_consumer", null);
            return dtResult;
        }

        public DataTable GetDashboardSummaryAsset()
        {
            DataTable dtResult = Common.CommonHelpers.GetDataTablePGFunction("_1_get_dashboardsummary_asset", null);
            return dtResult;
        }
        #endregion
        #endregion

    }
}
