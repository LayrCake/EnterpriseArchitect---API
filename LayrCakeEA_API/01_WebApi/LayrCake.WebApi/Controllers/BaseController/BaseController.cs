using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Serialize.Linq;
using Serialize.Linq.Serializers;
//using WebAppResources.Attributes;

namespace LayrCake.WebApi.Controllers.BaseControllers
{
    //[HandleErrorWithELMAH]
    public abstract partial class BaseController : BaseSystemController
    {
        public  ExpressionSerializer __expressionSerializer = new ExpressionSerializer(new XmlSerializer());

        //// GET: Base
        //public virtual ActionResult Index()
        //{
        //    return View();
        //}
    }
}