using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LayrCake.WebApi.Controllers.BaseControllers
{
    public partial class BaseSessionController : BaseController
    {
        public BaseSessionController()
        {
        //    if (Session != null && ViewBag != null && ViewBag.UserSessionVWM != null)
        //    {
        //        ViewBag.UserSessionVWM.SessionID = Session.SessionID.Trim() ?? String.Empty;
        //        //Todo: Set System Config value for Session Expire DateTime
        //        ViewBag.UserSessionVWM.SessionExpire = DateTime.Now.AddMinutes(Session.Timeout);
        //        ViewBag.UserSessionVWM.DisplayName = "Hugh";
        //        //Request.Form.GetValues(GlobalHiddenFields.DISPLAYNAME)
        //    }

        //    if (ViewBag == null || ViewBag.UserSessionVWM == null || String.IsNullOrEmpty(ViewBag.UserSessionVWM.SessionID))
        //    {
        //        //RedirectToAction(MVC.AppViews.Contacts());
        //        RedirectToAction(MVC.Account.Actions.Login_2());
        //    }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // If session exists
            if (filterContext.HttpContext.Session != null)
            {
                string cookie = filterContext.HttpContext.Request.Headers["Cookie"];

                //if new session
                if (filterContext.HttpContext.Session.IsNewSession && (cookie != null) && (cookie.IndexOf("ASP.NET_SessionId") >= 0))
                {

                    if ((cookie != null) && (cookie.IndexOf("ASP.NET_SessionId") < 1))
                    {
                        //redirect to desired session 
                        //expiration action and controller
               //Todo:         filterContext.Result = RedirectToAction(MVC.Account.Actions.Login());
                        return;
                    }
                    //if cookie exists and sessionid index is greater than zero
                    if ((cookie != null) && (cookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        //redirect to desired session 
                        //expiration action and controller
                        //filterContext.Result = RedirectToAction(MVC.Account.Actions.LockScreen());
                        return;
                    }
                }
            }

            //otherwise continue with action
            base.OnActionExecuting(filterContext);
        }
    }
}