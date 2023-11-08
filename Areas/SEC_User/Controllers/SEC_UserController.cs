using Metronic_8.Areas.SEC_User.Models;
using Metronic_8.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using System.Data;
using System.Text;

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

		#region Funtion: Convert String Password into Encrypted Password
		public static string EncryptPassword(string Password)
		{
			if (string.IsNullOrEmpty(Password))
			{
				return null;
			}
			else
			{
				byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(Password);
				string encryptedPassword = Convert.ToBase64String(storePassword);
				return encryptedPassword;
			}
		}
		#endregion

		#region Funtion: Convert Encrypted Password into Decrypted Password
		public static string DecryptPassword(string Password)
		{
			if (string.IsNullOrEmpty(Password))
			{
				return null;
			}
			else
			{
				byte[] encryptedPassword = Convert.FromBase64String(Password);
				string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
				return decryptedPassword;
			}
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
			//string encryptPassword = EncryptPassword(modelSEC_User.Password);
			//modelSEC_User.Password = encryptPassword;

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
				//string encryptPassword = EncryptPassword(modelSEC_User.Password);
				//modelSEC_User.Password = encryptPassword;

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
					TempData["Success"] = "Signin Successfully !";
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
