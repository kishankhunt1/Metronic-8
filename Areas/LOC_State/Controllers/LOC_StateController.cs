using Metronic_8.Areas.LOC_Country.Models;
using Metronic_8.Areas.LOC_State.Models;
using Metronic_8.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Metronic_8.Areas.LOC_State.Controllers
{
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
            ViewBag.List = CountryList;
            #endregion


            DataTable dt = dalLOC.PR_LOC_State_SelectAll(modelLOC_State);
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
            ViewBag.State = State;
            return View("StateList");
        }
        #endregion

        #region Function: Add Record
        public IActionResult Add(int? StateID)
        {
            if (ModelState.IsValid)
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
                ViewBag.List = CountryList;
                #endregion

                if (StateID != null)
                {
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
                //if record insert
                if (Convert.ToBoolean(dalLOC.PR_LOC_State_Insert(modelLOC_State)))
                {
                    TempData["success"] = "Record Inserted successfully";
                    return RedirectToAction("Index");

                }
            }
            else
            {
                //if record update
                if (Convert.ToBoolean(dalLOC.PR_LOC_State_UpdateByPk(modelLOC_State)))
                {
                    TempData["success"] = "Record Updated successfully.";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Add");
        }
        #endregion

        #region Function: Delete record
        public IActionResult Delete(int StateID)
        {
            if (Convert.ToBoolean(dalLOC.PR_LOC_State_DeleteByPk(StateID)))
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
