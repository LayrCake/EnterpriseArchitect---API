using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Plus.v1;
using Google.Apis.Plus.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using LayrCakeOnline.Helpers;
using MailClient.Helpers;
using MailClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailClient.APIRepositories
{
    public class GoogleMail
    {
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "LayrCake";
        UserCredential credential;
        GmailService serviceMail;
        PlusService servicePlus;

        public async void GoogleMailSetup() //<GmailService>
        {
            JsonFileOpener.OpenFile(@"D:\_01TestData\LayrCake.json");
            //var clientSecret = new ClientSecrets
            //{
            var CLIENT_ID = "871994902597-3bktld8de0211n3cquvenatqqn8ml794.apps.googleusercontent.com";
            var CLIENT_SECRET = "Ln0HRW4POvonG1LO2njNX3-f";
            //};

            AuthorizationBroker.RedirectUri = "http://localhost:58893/Mailbox/Inbox";

            string filename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\LayrCake.MailClient\Google.Apis.Auth.OAuth2.Responses.TokenResponse-" + Environment.UserName;

            var localfileDatastore = new LocalFileDataStore(filename);
            servicePlus = MailClient.APIRepositories.Authenticaton.AuthenticateOauth(CLIENT_ID, CLIENT_SECRET, "test", localfileDatastore);

            // Getting a list of ALL a users public activities.
            IList<Activity> _Activities = GetAllActivities(servicePlus, "me");

            foreach (Activity item in _Activities)
            {
                Console.WriteLine(item.Actor.DisplayName + " Plus 1s: " + item.Object__.Plusoners.TotalItems + " comments: " + item.Object__.Replies.TotalItems);
            }         
        }

        public List<EmailLabel> GoogleMail_Get_Labels(GmailService service, string _userId)
        {
            GoogleMailSetup();
    
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

        public static IList<Activity> GetAllActivities(PlusService service, string _userId)
        {
            //List all of the activities in the specified collection for the current user.  
            // Documentation: https://developers.google.com/+/api/latest/activities/list
            ActivitiesResource.ListRequest list = service.Activities.List(_userId, ActivitiesResource.ListRequest.CollectionEnum.Public__);
            list.MaxResults = 100;
            ActivityFeed activitesFeed = list.Execute();
            IList<Activity> Activites = new List<Activity>();

            //// Loop through until we arrive at an empty page
            while (activitesFeed.Items != null)
            {
                // Adding each item  to the list.
                foreach (Activity item in activitesFeed.Items)
                {
                    Activites.Add(item);
                }

                // We will know we are on the last page when the next page token is
                // null.
                // If this is the case, break.
                if (activitesFeed.NextPageToken == null)
                {
                    break;
                }

                // Prepare the next page of results
                list.PageToken = activitesFeed.NextPageToken;

                // Execute and process the next page request
                activitesFeed = list.Execute();

            }

            return Activites;
        }
    }
}
