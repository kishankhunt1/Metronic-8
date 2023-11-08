using Metronic_8.Areas.LOC_Country.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using Metronic_8.BAL;
using Metronic_8.Areas.LOC_State.Models;
using Metronic_8.Areas.LOC_City.Models;
using System.Data.SqlTypes;

namespace Metronic_8.DAL
{
    public class LOC_DALBase : DAL_Helper
    {
        #region Country Procedures

        #region Method: PR_LOC_Country_SelectAll
        public DataTable PR_LOC_Country_SelectAll(string CountryName)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                DataTable dt = new DataTable();

                if (CountryName != null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SearchForCountryName");
                    sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                    if (CountryName != null)
                        sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, CountryName);
                    else
                        sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, DBNull.Value);
                }

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
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
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
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

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
        public bool? PR_LOC_Country_UpdateByPk(LOC_CountryModel modelLOC_Country, int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_UpdateByPk");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

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
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());


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

        #region State Procedures

        #region Method: PR_LOC_State_SelectAll
        public DataTable PR_LOC_State_SelectAll(LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();
                if (modelLOC_State.CountryID != null || modelLOC_State.StateName != null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectByCountryNameStateName");
                    sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                    if (modelLOC_State.CountryID != null)
                        sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_State.CountryID);
                    else
                        sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, DBNull.Value);

                    if (modelLOC_State.StateName != null)
                        sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, modelLOC_State.StateName);
                    else
                        sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, DBNull.Value);

                }
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

        #region Method: PR_LOC_State_SelectAllForEditMultiple
        public DataTable PR_LOC_State_SelectAllForEditMultiple()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectAll");
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

        #region Method: PR_LOC_State_SelectByPk
        public DataTable PR_LOC_State_SelectByPk(int? StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectByPk");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
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

        #region Method: PR_LOC_State_Insert
        public bool? PR_LOC_State_Insert(LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.NVarChar, modelLOC_State.CountryID);
                sqlDB.AddInParameter(dbCMD, "Statename", SqlDbType.NVarChar, modelLOC_State.StateName);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                int result = sqlDB.ExecuteNonQuery(dbCMD);
                return (result == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: PR_LOC_State_UpdateByPk
        public bool? PR_LOC_State_UpdateByPk(LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_UpdateByPk");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_State.StateID);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_State.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, modelLOC_State.StateName);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                int result = sqlDB.ExecuteNonQuery(dbCMD);
                return (result == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: PR_LOC_State_DeleteByPk
        public bool? PR_LOC_State_DeleteByPk(int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_DeleteByPk");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

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

        #region City Procedures

        #region Method: PR_LOC_City_SelectAll
        public DataTable PR_LOC_City_SelectAll(LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectAll");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                DataTable dt = new DataTable();

                if (modelLOC_City.CountryID != null || modelLOC_City.StateID != null || modelLOC_City.CityName != null)
                {
                    dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectByCountryNameStateNameCityName");
                    sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                    if (modelLOC_City.CountryID != null)
                        sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_City.CountryID);
                    else
                        sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, DBNull.Value);

                    if (modelLOC_City.StateID != null)
                        sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_City.StateID);
                    else
                        sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, DBNull.Value);

                    if (modelLOC_City.CityName != null)
                        sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                    else
                        sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, DBNull.Value);

                }
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

        #region Method: PR_LOC_City_SelectByPk
        public DataTable PR_LOC_City_SelectByPk(int? CityID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectByPk");
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

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

        #region Method: PR_LOC_City_Insert
        public bool? PR_LOC_City_Insert(LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.NVarChar, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.NVarChar, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "Cityname", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());


                int result = sqlDB.ExecuteNonQuery(dbCMD);
                return (result == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: PR_LOC_City_UpdateByPk
        public bool? PR_LOC_City_UpdateByPk(LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_UpdateByPk");
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelLOC_City.CityID);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

                int result = sqlDB.ExecuteNonQuery(dbCMD);
                return (result == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: PR_LOC_City_DeleteByPk
        public bool? PR_LOC_City_DeleteByPk(int CityID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(ConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_DeleteByPk");
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());

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
