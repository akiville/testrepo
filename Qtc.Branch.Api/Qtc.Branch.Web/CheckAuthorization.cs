using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Qtc.qPos.Web
{
    public class CheckAuthorization : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String user_id = HttpContext.Current.Session["UserID"].ToString();


            if (HttpContext.Current.Session["UserID"] == null )
            {
                //if (filterContext.HttpContext.Request.IsAjaxRequest())
                //{
                //    filterContext.HttpContext.Response.StatusCode = 302; //Found Redirection to another page. Here- login page. Check Layout ajaxError() script.  
                //    filterContext.HttpContext.Response.End();
                //}
                //else
                //{
                    //filterContext.Result = new RedirectToRouteResult(  new RouteValueDictionary {{ "Login", "Index" } });
                    filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl.Replace(".aspx", "") + "?ReturnUrl=HOME");
                //}
            }
            else
            {

                //Code HERE for page level authorization  


                //Create a salt  
                //var salt = Get_SALT();

                //Create a hash  
                //var hash = Get_HASH_SHA512("Your Password", "Your UserName", salt)
            }
        }
    }
}