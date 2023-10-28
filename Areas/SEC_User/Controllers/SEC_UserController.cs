using Metronic_8.Areas.SEC_User.Models;
using Metronic_8.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Metronic_8.Areas.SEC_User.Controllers
{
    [Area("SEC_User")]
    [Route("[Controller]/[action]")]
    public class SEC_UserController : Controller
    {
        private readonly IConfiguration configuration;
        SEC_DAL dalSEC = new SEC_DAL();

        public SEC_UserController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

		#region Function: Signup
        public IActionResult Signup()
        {
            return View("Signup");
        }
        #endregion

        #region Function: Signup Save
        [HttpPost]
        public IActionResult SignupPOST(SEC_UserModel modelSEC_User)
        {
            if (Convert.ToBoolean(dalSEC.PR_SEC_User_InsertUser(modelSEC_User)))
            {
                TempData["Success"] = "Signup successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Signup");
        }
        #endregion

        #region Function: Login
        [HttpPost]
        public IActionResult Login(SEC_UserModel modelSEC_User)
        {
            String error = null;
            if (modelSEC_User.UserName == null)
            {
                error += "User Name is required";
            }
            if (modelSEC_User.Password == null)
            {
                error += "Password is required";
            }

            if (error != null)
            {
                TempData["InvalidCredentials"] = error;
                return RedirectToAction("Index");
            }
            else
            {
                DataTable dt=dalSEC.PR_SEC_User_SelectByUserNamePassword(modelSEC_User.UserName, modelSEC_User.Password);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("UserEmail", dr["UserEmail"].ToString());
                        HttpContext.Session.SetString("FirstName", dr["FirstName"].ToString());
                        HttpContext.Session.SetString("LastName", dr["LastName"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["InvalidCredentials"] = "User Name or Password is invalid!";
                    return RedirectToAction("Index");
                }

                if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Function: Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
