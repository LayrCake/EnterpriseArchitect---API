using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Controllers;

namespace LayrCake.WebApi
{
    public class AzureConfigProviderOverride : MobileAppControllerConfigProvider
    {
        public override void Configure(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            // then tweak the default behavior
            controllerSettings.Formatters.JsonFormatter.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            controllerSettings.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver(); // CamelCasePropertyNamesContractResolver();
            base.Configure(controllerSettings, controllerDescriptor);
        }
    }
}