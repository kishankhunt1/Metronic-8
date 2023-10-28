using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace Metronic_8.DAL
{
    public class LOC_DAL:LOC_DALBase
    {
        #region Method: PR_LOC_Country_CountryDropDown
        public DataTable PR_LOC_Country_CountryDropDown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_CountryDropDown");
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

        #region Method: PR_LOC_State_StateDropDown
        public DataTable PR_LOC_State_StateDropDown()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_StateDropDown");
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

        #region Method: PR_LOC_State_StateDropDownByCountryID
        public DataTable PR_LOC_State_StateDropDownByCountryID(int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_StateDropDownByCountryID");
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

        #region Method: PR_LOC_Country_SearchForCountryName

        public DataTable PR_LOC_Country_SearchForCountryName(string CountryName)
        {
            SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
            DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SearchForCountryName");
            sqlDB.AddInParameter(dbCMD, "@CountryName", SqlDbType.NVarChar, CountryName);
            DataTable dt = new DataTable();
            using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
            {
                dt.Load(dr);
            }
            return dt;
        }


        #endregion
    }
}
