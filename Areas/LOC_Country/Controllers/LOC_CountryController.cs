using Metronic_8.Areas.LOC_Country.Models;
using Metronic_8.BAL;
using Metronic_8.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Metronic_8.Areas.LOC_Country.Controllers
{
	[CheckAccess]
	[Area("LOC_Country")]
    [Route("[Controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        LOC_DAL dalLOC = new LOC_DAL();

        #region Funtion: Select All Record
        public IActionResult Index()
        {
            DataTable dt = dalLOC.PR_LOC_Country_SelectAll();
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
            ViewBag.Country = Country;
            return View("CountryList");
        }
        #endregion

        #region Function: Add Record
        public IActionResult Add(int? CountryID)
        {
            if (ModelState.IsValid)
            {
                if (CountryID != null)
                {
                    //if update the record
                    DataTable dt = dalLOC.PR_LOC_Country_SelectByPk(CountryID);

                    LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"].ToString());
                        modelLOC_Country.CountryName = dr["CountryName"].ToString();
                    }
                    return View("CountryAddEdit", modelLOC_Country);
                }
            }
            return View("CountryAddEdit");
        }
        #endregion

        #region Function: Save the record
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            if (ModelState.IsValid)
            {
                if (modelLOC_Country.CountryID == null)
                {
                    //if record insert
                    if (Convert.ToBoolean(dalLOC.PR_LOC_Country_Insert(modelLOC_Country)))
                    {
                        TempData["success"] = "Record Inserted successfully";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //if record update
                    if (Convert.ToBoolean(dalLOC.PR_LOC_Country_UpdateByPk(modelLOC_Country)))
                    {
                        TempData["success"] = "Record Updated successfully.";
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Add");
        }
        #endregion

        #region Function: Delete record
        public IActionResult Delete(int CountryID)
        {
            if (Convert.ToBoolean(dalLOC.PR_LOC_Country_DeleteByPk(CountryID)))
            {
                TempData["success"] = "Record deleted successfully.";
            }
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
                TempData["success"] = "Records deleted successfully.";
                result = "success";
            }
            return new JsonResult(result);
        }
        #endregion

        #region Function: Search Record
        public IActionResult Search(string CountryName)
        {
            DataTable dt = dalLOC.PR_LOC_Country_SearchForCountryName(CountryName);
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
                //CountryModel.UserID = Convert.ToInt32(dr["UserID"]);
                Country.Add(CountryModel);
            }
            ViewBag.Country = Country;
            return View("CountryList");
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
