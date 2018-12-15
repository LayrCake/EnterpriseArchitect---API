using Google.Apis.Calendar.v3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Google.Apis.Gmail.v1;

namespace MailClient.Services
{
    public class LayrCakeGCalenderService
    {
        public CalendarService CreateCalendar(ClaimsIdentity identity)
        {
            var credential = new LayrCakeGoogleService().GetCredentialForApiAsync(identity).Result;
            var initializer = new LayrCakeGoogleInitialize().Initialize(credential);
            return new CalendarService(initializer);
        }

        public GmailService CreateGmail(ClaimsIdentity identity)
        {
            var credential = new LayrCakeGoogleService().GetCredentialForApiAsync(identity).Result;
            var initializer = new LayrCakeGoogleInitialize().Initialize(credential);
            return new GmailService(initializer);
        }

    }
}
