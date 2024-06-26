﻿using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CUMI.Controllers
{
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            object varJG0 = httpContext.Session["UserName"];
            return httpContext.Session["UserName"] != null;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "Login",
                action = "Index"
            }));
            filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
        }
    }
}
