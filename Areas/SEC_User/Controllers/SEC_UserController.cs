using Metronic_8.Areas.SEC_User.Models;
using Metronic_8.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
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

        #region Function: Convert Password into PasswordHash
        private string HashPassword(string password)
        {
            // Hash the password using a suitable hashing library (e.g., BCrypt)
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }
		#endregion

		#region Function: Index
		public IActionResult Index()
		{
			return View();
		}
        #endregion

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
            //string hashedPassword = HashPassword(modelSEC_User.Password); // Hash the password
            //modelSEC_User.Password = hashedPassword; // Update the model with the hashed password

            if (Convert.ToBoolean(dalSEC.PR_SEC_User_InsertUser(modelSEC_User)))
			{
				TempData["SignUpSuccess"] = "Signup successfully";
				return RedirectToAction("Index");
			}
			else
			{
				TempData["SignUpError"] = "Username already Exists";
				return RedirectToAction("Signup");

			}
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
                //// Hash the provided password
                //string hashedPassword = HashPassword(modelSEC_User.Password);

                DataTable dt = dalSEC.PR_SEC_User_SelectByUserNamePassword(modelSEC_User.UserName, modelSEC_User.Password);
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
                    TempData["Success"] = "Signin successfully";
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
