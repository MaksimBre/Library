using System;
using System.Web;
using System.Web.Security;

namespace Library.PresentationLayer.Web.Helpers
{
    public static class UserIdentity
    {
        public static bool IsInRole(string askRole)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var cookieValue = authCookie.Value;
                if (!String.IsNullOrWhiteSpace(cookieValue))
                {
                    var ticket = FormsAuthentication.Decrypt(cookieValue).UserData.ToString();
                    string[] roles = ticket.Split(',');
                    string[] inputRoles = askRole.Split(',');
                    for (var i=0; i<roles.Length; i++)
                    {
                        for (var j = 0; j < inputRoles.Length; j++)
                        {
                            if (roles[i] == inputRoles[j]) return true;
                        }
                    }
                }

            }

            return false;
        }

        public static string UserName()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var cookieValue = authCookie.Value;
                if (!String.IsNullOrWhiteSpace(cookieValue))
                {
                    string[] ticket = FormsAuthentication.Decrypt(cookieValue).Name.ToString().Split('/');
                    return ticket[1];
                }
            }
                    return "Error!";
        }
        public static string Email()
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var cookieValue = authCookie.Value;
                if (!String.IsNullOrWhiteSpace(cookieValue))
                {
                    string[] ticket = FormsAuthentication.Decrypt(cookieValue).Name.ToString().Split('/');
                    return ticket[0];
                }
            }
            return "Error!";
        }
    }
}