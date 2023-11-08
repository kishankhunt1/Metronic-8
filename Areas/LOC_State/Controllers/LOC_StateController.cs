using Metronic_8.Areas.LOC_Country.Models;
using Metronic_8.Areas.LOC_State.Models;
using Metronic_8.BAL;
using Metronic_8.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Metronic_8.Areas.LOC_State.Controllers
{
    [CheckAccess]
    [Area("LOC_State")]
	[Route("[Controller]/[action]")]
	public class LOC_StateController : Controller
	{
        LOC_DAL dalLOC = new LOC_DAL();

        #region Function: Select All Record
        public IActionResult Index(LOC_StateModel modelLOC_State)
        {
            #region  Country Drop down
            DataTable dt1 = dalLOC.PR_LOC_Country_CountryDropDown();
            List<CountryDropDown> CountryList = new List<CountryDropDown>();
            foreach (DataRow dr in dt1.Rows)
            {
                CountryDropDown vlst = new CountryDropDown();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                CountryList.Add(vlst);
            }
            ViewBag.CountryList = CountryList;
            #endregion

            DataTable dt = dalLOC.PR_LOC_State_SelectAll(modelLOC_State);

            #region Fill the Record into List
            List<LOC_StateModel> State = new List<LOC_StateModel>();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_StateModel StateModel = new LOC_StateModel();
                StateModel.StateID = Convert.ToInt32(dr["StateID"]);
                StateModel.CountryName = dr["CountryName"].ToString();
                StateModel.StateName = dr["StateName"].ToString();
                StateModel.Created = Convert.ToDateTime(dr["Created"]);
                StateModel.Modified = Convert.ToDateTime(dr["Modified"]);
                State.Add(StateModel);
            }
            ViewBag.StateModel = State;
            #endregion

            return View("StateList");
        }
        #endregion

        #region Function: Add Record
        public IActionResult Add(int? StateID)
        {
            if (ModelState.IsValid)
            {
                #region Form Title
                TempData["Action"] = "Add";
                #endregion

                #region  Country Drop down
                DataTable dt1 = dalLOC.PR_LOC_Country_CountryDropDown();
                List<CountryDropDown> CountryList = new List<CountryDropDown>();
                foreach (DataRow dr in dt1.Rows)
                {
                    CountryDropDown vlst = new CountryDropDown();
                    vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                    vlst.CountryName = dr["CountryName"].ToString();
                    CountryList.Add(vlst);
                }
                ViewBag.CountryList = CountryList;
                #endregion

                if (StateID != null)
                {
                    #region Form Title
                    TempData["Action"] = "Edit";
                    #endregion

                    #region Update record
                    //if update the record
                    DataTable dt = dalLOC.PR_LOC_State_SelectByPk(StateID);

                    LOC_StateModel modelLOC_State = new LOC_StateModel();

                    foreach (DataRow dr in dt.Rows)
                    {
                        modelLOC_State.StateID = Convert.ToInt32(dr["StateID"].ToString());
                        modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"].ToString());
                        modelLOC_State.StateName = dr["StateName"].ToString();
                    }
                    return View("StateAddEdit", modelLOC_State);
                    #endregion
                }
            }
            return View("StateAddEdit");
        }
        #endregion

        #region Function: Save the record
        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            if (modelLOC_State.StateID == null)
            {
                #region Inserting record
                //if record insert
                if (Convert.ToBoolean(dalLOC.PR_LOC_State_Insert(modelLOC_State)))
                {
                    TempData["success"] = "Record Inserted successfully";
                    return RedirectToAction("Index");

                }
                #endregion
            }
            else
            {
                #region Updating record
                //if record update
                if (Convert.ToBoolean(dalLOC.PR_LOC_State_UpdateByPk(modelLOC_State)))
                {
                    TempData["success"] = "Record Updated successfully.";
                    return RedirectToAction("Index");
                }
                #endregion
            }
            return RedirectToAction("Add");
        }
        #endregion

        #region Function: Delete record
        public IActionResult Delete(int StateID)
        {
            #region Deleting record
            if (Convert.ToBoolean(dalLOC.PR_LOC_State_DeleteByPk(StateID)))
            {
                TempData["success"] = "Record deleted successfully.";
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
                    dalLOC.PR_LOC_State_DeleteByPk(id);
                }
                TempData["success"] = "Records deleted successfully.";
                result = "success";
            }
            return new JsonResult(result);
        }
        #endregion

        #region Function: Clear Search Result
        public IActionResult Clear()
        {
            return RedirectToAction("Index");
        }
        #endregion

	}
}
