using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using Metronic_8.Areas.LOC_Country.Models;
using Metronic_8.Areas.SEC_User.Models;

namespace Metronic_8.DAL
{
    public class SEC_DALBase:DAL_Helper
    {
        #region Function: PR_SEC_User_SelectByUserNamePassword
        public DataTable PR_SEC_User_SelectByUserNamePassword(string UserName,string Password)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_User_SelectByUserNamePassword");
                sqlDB.AddInParameter(dbCMD, "@UserName", SqlDbType.NVarChar, UserName);
                sqlDB.AddInParameter(dbCMD, "@Password", SqlDbType.NVarChar, Password);

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
		#endregion

		#region Function: PR_SEC_User_InsertUser
		public bool? PR_SEC_User_InsertUser(SEC_UserModel modelSEC_User)
		{
			try
			{
				SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
				DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_SEC_User_InsertUser");
				sqlDB.AddInParameter(dbCMD, "@UserName", SqlDbType.NVarChar, modelSEC_User.UserName);
                sqlDB.AddInParameter(dbCMD, "@Password", SqlDbType.NVarChar, modelSEC_User.Password); // This should be the hashed password
                sqlDB.AddInParameter(dbCMD, "@FirstName", SqlDbType.NVarChar, modelSEC_User.FirstName);
				sqlDB.AddInParameter(dbCMD, "@LastName", SqlDbType.NVarChar, modelSEC_User.LastName);
				sqlDB.AddInParameter(dbCMD, "@UserEmail", SqlDbType.NVarChar, modelSEC_User.UserEmail);

				int result = sqlDB.ExecuteNonQuery(dbCMD);
				return (result == -1 ? false : true);
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		#endregion

		
	}
}
