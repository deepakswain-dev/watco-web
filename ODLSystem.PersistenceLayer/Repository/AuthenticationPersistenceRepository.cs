#region Copyright
/******************************************************************************
* Copyright (C) 2018-2022 MicroGlobalTranz - All Rights Reserved.
*
* Proprietary and confidential. Unauthorized copying of this file, via any
* medium is strictly prohibited without the explicit permission of MicroGlobalTranz.
* Powered By Deepak Swain
******************************************************************************/
#endregion Copyright

using ODLSystem.DatabaseHelper;
using ODLSystem.Library.Common.EntityModel;
using ODLSystem.PersistenceLayer.Repository.Intrefaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODLSystem.PersistenceLayer.Repository
{
    public class AuthenticationPersistenceRepository : IAuthenticationPersistenceRepository
    {
        DatabaseHelpersEngine objDatabaseHelpersEngine = null;

        public AuthenticationPersistenceRepository()
        {
            objDatabaseHelpersEngine = new DatabaseHelpersEngine();
        }

        public UsersEntityModel ValidateUser(string username, string password)
        {

            UsersEntityModel objUsersEntityModel = new UsersEntityModel();

            DataTable objDataTable = PopulateUserDetails(username, password);

            if (objDataTable != null)
            {
                foreach (DataRow dr in objDataTable.Rows)
                {
                    objUsersEntityModel.UserId = (dr["UserId"] == System.DBNull.Value) ? (Int32)0 : Convert.ToInt32(dr["UserId"]);
                    objUsersEntityModel.UserName = (dr["UserName"] == System.DBNull.Value) ? (string)null : Convert.ToString(dr["UserName"]);
                    objUsersEntityModel.UserPassword = (dr["UserPassword"] == System.DBNull.Value) ? (string)null : Convert.ToString(dr["UserPassword"]);
                    objUsersEntityModel.UserFullName = (dr["UserFullName"] == System.DBNull.Value) ? (string)null : Convert.ToString(dr["UserFullName"]);
                    objUsersEntityModel.UserEmail = (dr["UserEmailId"] == System.DBNull.Value) ? (string)null : Convert.ToString(dr["UserEmailId"]);
                    objUsersEntityModel.UserPhone = (dr["UserPhone"] == System.DBNull.Value) ? (string)null : Convert.ToString(dr["UserPhone"]);
                    objUsersEntityModel.CityId = (dr["CityId"] == System.DBNull.Value) ? (Int32)0 : Convert.ToInt32(dr["CityId"]);
                    objUsersEntityModel.CityName = (dr["CityName"] == System.DBNull.Value) ? (string)null : Convert.ToString(dr["CityName"]);
                }
            }

            return objUsersEntityModel;

        }

        private DataTable PopulateUserDetails(string username, string password)
        {
            DataTable objDataTable = new DataTable();
            try
            {
                //DbParameter[] dbParams = new DbParameter[]
                //{
                //    new SqlParameter("@UserName", username),
                //    new SqlParameter("@Password", password)
                //};
                //objDataTable = objDatabaseHelpersEngine.GetDataTable(DBConstants.ValidateUserCredential, dbParams);
                objDataTable = objDatabaseHelpersEngine.GetUserDetails(username,password);
            }
            catch (Exception)
            {
                throw;
            }

            return objDataTable;
        }
    }
}
