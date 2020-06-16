using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace ODLSystem.DatabaseHelper.Common
{
    internal class CommonHelpers
    {
        private static readonly DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        private static readonly string ODLConnectionStringPG = ConfigurationManager.ConnectionStrings["ODLConnectionPG"].ConnectionString;

        #region PostgreSQL Helpers
        internal static DataTable GetDataTablePGQuery(string qryString)
        {
            DataTable objDataTable = new DataTable();
            try
            {
                using (NpgsqlConnection objSqlPGConnection = new NpgsqlConnection(ODLConnectionStringPG))
                {
                    if (objSqlPGConnection.State != ConnectionState.Open) objSqlPGConnection.Open();

                    using (NpgsqlCommand objSqlPGCommand = new NpgsqlCommand(qryString, objSqlPGConnection))
                    {
                        using (NpgsqlDataReader ndr = objSqlPGCommand.ExecuteReader(CommandBehavior.Default))
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

        internal static DataTable GetDataTablePGFunction(string ProcedureName, DbParameter[] dbParams)
        {
            DataTable dt = new DataTable();
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(ODLConnectionStringPG))
                {
                    if (con.State != ConnectionState.Open) con.Open();

                    using (NpgsqlCommand cmd = new NpgsqlCommand(ProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (dbParams != null)
                        {
                            cmd.Parameters.AddRange(dbParams);
                        }

                        using (NpgsqlDataReader ndr = cmd.ExecuteReader(CommandBehavior.Default))
                        {
                            if (ndr.HasRows)
                            {
                                dt.Load(ndr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
            return dt;
        }
        #endregion
    }
}
