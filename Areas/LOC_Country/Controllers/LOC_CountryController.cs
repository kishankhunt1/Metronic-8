using Metronic_8.Areas.LOC_Country.Models;
using Metronic_8.BAL;
using Metronic_8.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlTypes;

namespace Metronic_8.Areas.LOC_Country.Controllers
{
    [CheckAccess]
    [Area("LOC_Country")]
    [Route("[Controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        LOC_DAL dalLOC = new LOC_DAL();

        #region Funtion: Select All Record
        public IActionResult Index(string CountryName)
        {
            DataTable dt = dalLOC.PR_LOC_Country_SelectAll(CountryName);

            #region Fill the record into List
            List<LOC_CountryModel> Country = new List<LOC_CountryModel>();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_CountryModel CountryModel = new LOC_CountryModel();
                CountryModel.CountryID = Convert.ToInt32(dr["CountryID"]);
                CountryModel.CountryName = dr["CountryName"].ToString();
                CountryModel.StateCount = Convert.ToInt32(dr["StateCount"]);
                CountryModel.CityCount = Convert.ToInt32(dr["CityCount"]);
                CountryModel.Created = Convert.ToDateTime(dr["Created"]);
                CountryModel.Modified = Convert.ToDateTime(dr["Modified"]);

                Country.Add(CountryModel);
            }
            ViewBag.CountryModel = Country;
            #endregion

            return View("CountryList");
        }
        #endregion

        #region Function: Add Record
        public IActionResult Add(string? CountryID)
        {
            if (ModelState.IsValid)
            {
                #region Form Title
                TempData["Action"] = "Add";
                #endregion

                if (CountryID != null)
                {
                    #region Form Title
                    TempData["Action"] = "Edit";
                    #endregion

                    #region Decrypt the Id
                    SqlInt32 decryptedID = CommonFunctions.DecryptBase64Int32(CountryID);
                    int id = decryptedID.Value;
                    #endregion

                    #region Update record
                    //if update the record
                    DataTable dt = dalLOC.PR_LOC_Country_SelectByPk(id);
                    LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"].ToString());
                        modelLOC_Country.CountryName = dr["CountryName"].ToString();
                    }
                    return View("CountryAddEdit", modelLOC_Country);
                    #endregion
                }
            }
            return View("CountryAddEdit");
        }
        #endregion

        #region Function: Save the record
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country, string CountryID)
        {
            if (modelLOC_Country.CountryID == null)
            {
                if (CountryID == null)
                {
                    #region Inserting Record
                    //if record insert
                    if (Convert.ToBoolean(dalLOC.PR_LOC_Country_Insert(modelLOC_Country)))
                    {
                        TempData["success"] = "Record Inserted Successfully !";
                        return RedirectToAction("Index");
                    }
                    #endregion
                }
                else
                {
                    #region Decrypt Id
                    SqlInt32 decryptedID = CommonFunctions.DecryptBase64Int32(CountryID);
                    int id = decryptedID.Value;
                    #endregion

                    #region Updating Record
                    //if record update
                    if (Convert.ToBoolean(dalLOC.PR_LOC_Country_UpdateByPk(modelLOC_Country, id)))
                    {
                        TempData["success"] = "Record Updated Successfully !";
                        return RedirectToAction("Index");
                    }
                    #endregion
                }
            }
            return RedirectToAction("Add");
        }
        #endregion

        #region Function: Delete record
        public IActionResult Delete(string CountryID)
        {
            #region Decrypt the Id
            SqlInt32 decryptedID = CommonFunctions.DecryptBase64Int32(CountryID);
            int id = decryptedID.Value;
            #endregion

            #region Deleteing Record
            if (Convert.ToBoolean(dalLOC.PR_LOC_Country_DeleteByPk(id)))
            {
                TempData["success"] = "Record deleted Successfully !";
            }
            #endregion

            return RedirectToAction("Index");
        }
        #endregion

        #region Function: Delete Multiple
        [HttpPost]
        public IActionResult DeleteMultiple(int[] Ids)
        {
            string result = string.Empty;
            if (Ids.Count() > 0)
            {
                foreach (int id in Ids)
                {
                    dalLOC.PR_LOC_Country_DeleteByPk(id);
                }
                TempData["success"] = "Records deleted Successfully !";
                result = "success";
            }
            return new JsonResult(result);
        }
        #endregion

        #region Funtion: Clear Search Result
        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
        #endregion
    }
}
