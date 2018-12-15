using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LayrCake.WebApi.Controllers.BaseControllers
{
    public class ApplicationController<TSource> : Controller
    {
        private const string LogOnSession = "LogOnSession";  //session index name
        private const string ErrorController = "Error";      //session independent controller
        private const string LogOnController = "LogOn";      //session independent and LogOn controller    
        private const string LogOnAction = "LogOn";          //action to rederect

        protected ApplicationController()
        {
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            /*important to check both, because logOn and error Controller should be access able with out any session*/
            if (!IsNonSessionController(requestContext) && !HasSession())
            {
                //Redirect to logon action
                Redirect(requestContext, Url.Action(LogOnAction, LogOnController));
            }
        }

        private bool IsNonSessionController(RequestContext requestContext)
        {
            var currentController = requestContext.RouteData.Values["controller"].ToString().ToLower();
            var nonSessionedController = new List<string>() { ErrorController.ToLower(), LogOnController.ToLower() };
            return nonSessionedController.Contains(currentController);
        }

        private void Redirect(RequestContext requestContext, string action)
        {
            requestContext.HttpContext.Response.Clear();
            requestContext.HttpContext.Response.Redirect(action);
            requestContext.HttpContext.Response.End();
        }

        protected bool HasSession()
        {
            return Session[LogOnSession] != null;
        }

        protected TSource GetLogOnSessionModel()
        {
            return (TSource)this.Session[LogOnSession];
        }

        protected void SetLogOnSessionModel(TSource model)
        {
            Session[LogOnSession] = model;
        }

        protected void AbandonSession()
        {
            if (HasSession())
            {
                Session.Abandon();
            }
        }
    }
}