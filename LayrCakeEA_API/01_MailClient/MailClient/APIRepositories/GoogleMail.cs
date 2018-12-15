using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using LayrCakeOnline.Helpers;
using MailClient.Helpers;
using MailClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailClient.APIRepositories
{
    public class GoogleMailService
    {
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "LayrCake";
        UserCredential credential;
        GmailService service;

        public async Task GoogleMailSetup() //<GmailService>
        {
            //var json = JsonFileOpener.OpenFile(@"D:\_01TestData\LayrCake.json");
            //var list = JsonConvert.DeserializeObject<List<Models.GoogleMail>>(json);

            //var clientSecret = new ClientSecrets
            //{
            //    ClientId = "871994902597-3bktld8de0211n3cquvenatqqn8ml794.apps.googleusercontent.com",
            //    ClientSecret = "Ln0HRW4POvonG1LO2njNX3-f"
            //};

            AuthorizationBroker.RedirectUri = "http://localhost:58893/Mailbox/InboxLoginSuccess";

            using (var stream = new FileStream(@"D:\_01TestData\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart");

                credential = AuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
            }

            service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public List<EmailMessage> GoogleMail_Get_Mail(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                // Create Gmail API Service
                service = service == null ? new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                }) : service;

                // Define parameters of request
                var request = service.Users.Messages.List("me");
                request.MaxResults = 10;

                var messageResponse = request.Execute();
                IList<Message> messages = messageResponse.Messages;
                //var x = new Google.Apis.Gmail.v1.Data.MessagePartBody().
                //var message = new Message() { ;
                var messageList = AutoMapper.Mapper.Map<List<EmailMessage>>(messages);
                
                return messageList;
            }
            catch(Exception exception)
            {
                errorMessage = exception.InnerException != null ? exception.InnerException.Message : exception.Message;
                return null;
            }
        }

        public List<EmailLabel> GoogleMail_Get_Labels()
        {
            //using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            //{
            //    string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //    credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart");

            //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        GoogleClientSecrets.Load(stream).Secrets,
            //        Scopes,
            //        "Hugh.Proctor@gmail.com",
            //        CancellationToken.None,
            //        new FileDataStore(credPath, true)).Result;
            //    Debug.WriteLine("Credential file saved to: " + credPath);
            //}

            // Create Gmail API Service
            service = service == null ? new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                }) : service;

            // Define parameters of request
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

            // List Labels
            IList<Label> labels = request.Execute().Labels;

            var labelList = AutoMapper.Mapper.Map<List<EmailLabel>>(labels);

            //Debug.WriteLine("Labels:");
            //if (labels != null && labels.Any())
            //{
            //    foreach (var labelItem in labels)
            //        Debug.WriteLine("{0}", labelItem.Name);
            //}
            //else
            //    Debug.WriteLine("No Labels found");

            return labelList;
        }
    }
}
