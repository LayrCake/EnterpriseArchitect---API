using System.Web.Http;
using LayrCake.StaticModel.ActionServiceReference;
using Microsoft.Azure.Mobile.Server.Config;

namespace LayrCake.WebApi.Controllers
{
    [MobileAppController]
    public class DDDPackageController : AzureTableController<DDDPackage>
    {
        // GET api/DDDPackage
        public string Get()
        {
            return "Hello from custom controller!";
        }
    }
}
