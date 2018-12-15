using System;
using System.Configuration;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Configuration;
using LayrCake.StaticModel.DataVisualiserServiceReference;

namespace LayrCake.StaticModel.StaticModelReserved
{
    /// <summary>
    /// Static Request Helper class.
    /// Provides common functionalities that apply to all Request types.
    /// </summary>
    public static class RequestHelper
    {
        public static string ClientTag { get; private set; }

        /// <summary>
        /// Static constructor. Sets the ClientTag (read from web.config).
        /// </summary>
        static RequestHelper()
        {
            //Assembly.GetExecutingAssembly()
            //ClientTag = WebConfigurationManager.AppSettings.Get("ClientTag");
            //ClientTag = WebConfigurationManager.AppSettings["ClientTag"];
            Assembly me = Assembly.GetAssembly(typeof(RequestHelper));
            var config = ConfigurationManager.OpenExeConfiguration(System.IO.Path.Combine(me.Location));
            if (config.HasFile || config.AppSettings == null || config.AppSettings.Settings["ClientTag"] == null || string.IsNullOrEmpty(config.AppSettings.Settings["ClientTag"].Value))
                ClientTag = WebConfigurationManager.AppSettings.Get("ClientTag");
            else
                ClientTag = config.AppSettings.Settings["ClientTag"].Value;

            //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //AppSettingsSection section = config.AppSettings;
            //ClientTag = section.Settings["ClientTag"].Value;
        }

        /// <summary>
        /// Gets or sets the Access Token value (provided by Server and stored in Session).
        /// </summary>
        public static string AccessToken
        {
            get
            {
                if (HttpContext.Current == null)
                    return new AuthRepository().GetToken();
                if (HttpContext.Current.Session == null)
                    return new AuthRepository().GetToken();
                if (HttpContext.Current.Session["AccessToken"] == null)
                {
                    // Request a unique accesstoken from the webservice. This token is
                    // valid for the duration of the session.

                    var repository = new AuthRepository();
                    HttpContext.Current.Session["AccessToken"] = repository.GetToken();
                }
                return (string)HttpContext.Current.Session["AccessToken"];
            }
        }

        /// <summary>
        /// Helper extension method that adds RequestId, ClientId, and AccessToken to all request types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static T Prepare<T>(this T request) where T : RequestBase
        {
            request.RequestId = RequestId;
            request.ClientTag = ClientTag;
            request.AccessToken = AccessToken;

            return request;
        }

        /// <summary>
        /// Generates unique request GUID identifier.
        /// </summary>
        public static string RequestId
        {
            get { return Guid.NewGuid().ToString(); }
        }
    }
}