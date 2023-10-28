using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

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
    }
}
