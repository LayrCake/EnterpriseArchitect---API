using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Services
{
    public class LayrCakeGoogleInitialize
    {
        public BaseClientService.Initializer Initialize(UserCredential credential)
        {
            return new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "LayrCake",
            };
        }
    }
}
