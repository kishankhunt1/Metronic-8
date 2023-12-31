﻿namespace Metronic_8.BAL
{
	public static class CV
	{
		private static IHttpContextAccessor _httpContextAccessor;
        public static Boolean IsURLEncryption = true;

        static CV()
		{
			_httpContextAccessor = new HttpContextAccessor();
		}

		public static string? UserName()
		{
			string? UserName = null;

			if(_httpContextAccessor.HttpContext.Session.GetString("UserName") != null)
			{
				UserName = _httpContextAccessor.HttpContext.Session.GetString("UserName").ToString();
			}
			return UserName;
		}

        public static string? UserEmail()
        {
            string? UserEmail = null;

            if (_httpContextAccessor.HttpContext.Session.GetString("UserEmail") != null)
            {
                UserEmail = _httpContextAccessor.HttpContext.Session.GetString("UserEmail").ToString();
            }
            return UserEmail;
        }

        public static int? UserID()
		{
			int? UserID = null;

			if (_httpContextAccessor.HttpContext.Session.GetString("UserID") != null)
			{
				UserID = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("UserID").ToString());
			}
			return UserID;
		}
	}
}
