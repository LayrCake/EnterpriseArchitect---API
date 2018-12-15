using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Castle.Windsor;
using LayrCake.WebApi.ApiReserved;
using LayrCake.StaticModel;
using System.Diagnostics;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server.Tables.Config;
using System.Web.Http.OData.Extensions;
using Microsoft.Azure.Mobile.Server;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor.Installer;

namespace LayrCake.WebApi
{
    public static class WebApiConfig
    {
        internal static bool ModelsLoaded;
        private static IWindsorContainer _container;

        public static void Register(HttpConfiguration _config)
        {
            HttpConfiguration config = _config ?? new HttpConfiguration();

            // Web API configuration and services
            //Todo: Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            // Web API routes
            config.MapHttpAttributeRoutes();
            //config.EnableSystemDiagnosticsTracing();
            //var cors = new EnableCorsAttribute("http://192.168.0.142:2244/", "*", "*");
            //config.EnableCors(cors);
            new ClassAssemblyLoad();

            ConfigureWindsor();
            ModelsLoaded = MapperLoad.MapperLoadObjects();
            if (ModelsLoaded)
            {
                Debug.WriteLine("LayrCake WebApi Models loaded");
                //return;
            }
            config.Routes.MapHttpRoute("custom", ".auth/login/custom", new { controller = "Account" });
            new MobileAppConfiguration()
                //.UseDefaultConfiguration()
                //.AddMobileAppHomeController()
                //.MapApiControllers()
                .WithMobileAppControllerConfigProvider(new AzureConfigProviderOverride())
                .AddTables(
                    new MobileAppTableConfiguration()
                        .MapTableControllers()
                            //.AddEntityFramework()
                        )
                            //.AddPushNotifications()
                .ApplyTo(config);
            config.AddODataQueryFilter();
            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }

        private static void ConfigureWindsor()
        {
            _container = new WindsorContainer(new XmlInterpreter("WindsorConfig.xml")).Install(FromAssembly.This());
            _container = new WindsorContainer().Install(new ApiControllersInstaller());

            GlobalConfiguration.Configuration.DependencyResolver = new WindsorDependencyResolver(_container);
        }
    }
}
