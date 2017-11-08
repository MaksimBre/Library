using Library.BusinessLogicLayer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.PresentationLayer.Web.Helpers
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        Users context = new Users();
         
        private readonly string[] allowedroles;

        public CustomAuthorize(params string[] roles)
        {
            this.allowedroles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            foreach (var role in allowedroles)
            {
                
                if (UserIdentity.IsInRole(role))
                {
                    authorize = true; 
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}