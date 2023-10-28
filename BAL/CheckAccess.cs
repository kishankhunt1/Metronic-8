using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Metronic_8.BAL
{
	public class CheckAccess : ActionFilterAttribute, IAuthorizationFilter
	{
			//When UserID is not there then it will be redirect to login page
		public void OnAuthorization(AuthorizationFilterContext filterContext)
		{
			if (filterContext.HttpContext.Session.GetString("UserID") == null)
			{
				filterContext.Result = new RedirectResult("~/SEC_User/Index");
			}
		}

		//once Logout We can got go back to the back/Previous pages, we must need to do login again
		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			filterContext.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
			filterContext.HttpContext.Response.Headers["Expires"] = "-1";
			filterContext.HttpContext.Response.Headers["Pragma"] = "no-cache";
			base.OnResultExecuting(filterContext);
		}
	}
}
