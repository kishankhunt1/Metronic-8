using Metronic_8.Areas.LOC_Country.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Metronic_8.DAL
{
    public class LOC_DALBase:DAL_Helper
    {
        #region Country Procedures

        #region Method: PR_LOC_Country_SelectAll
        public DataTable PR_LOC_Country_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectAll");
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

        #region Method: PR_LOC_Country_SelectByPk
        public DataTable PR_LOC_Country_SelectByPk(int? CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectByPk");
                sqlDB.AddInParameter(dbCMD, "@CountryID", SqlDbType.Int, CountryID);
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

        #region Method: PR_LOC_Country_Insert
        public bool? PR_LOC_Country_Insert(LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_Insert");
                sqlDB.AddInParameter(dbCMD, "@CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);

                int result = sqlDB.ExecuteNonQuery(dbCMD);
                return (result == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: PR_LOC_Country_UpdateByPk
        public bool? PR_LOC_Country_UpdateByPk(LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_UpdateByPk");
                sqlDB.AddInParameter(dbCMD, "@CountryID", SqlDbType.Int, modelLOC_Country.CountryID);
                sqlDB.AddInParameter(dbCMD, "@CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);

                int result = sqlDB.ExecuteNonQuery(dbCMD);
                return (result == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: PR_LOC_Country_DeleteByPk
        public bool? PR_LOC_Country_DeleteByPk(int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_DeleteByPk");
                sqlDB.AddInParameter(dbCMD, "@CountryID", SqlDbType.Int, CountryID);

                int result = sqlDB.ExecuteNonQuery(dbCMD);
                return (result == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #endregion
    }
}
