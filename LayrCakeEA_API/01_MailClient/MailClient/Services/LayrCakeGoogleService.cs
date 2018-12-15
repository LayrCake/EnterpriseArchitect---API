using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using MailClient.APIRepositories;
using MailClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Google.Apis.Auth.OAuth2.Responses;

namespace MailClient.Services
{
    public class LayrCakeGoogleService
    {
        private readonly IDataStore dataStore = new LayrCakeDataStore();

        public async Task<UserCredential> GetCredentialForApiAsync(ClaimsIdentity identity)
        {
            var initializer = new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = MyClientSecrets.ClientId,
                    ClientSecret = MyClientSecrets.ClientSecret,
                },
                Scopes = MyRequestedScopes.Scopes,
            };
            var flow = new GoogleAuthorizationCodeFlow(initializer);

            var googleUserId = identity.FindFirstValue(MyClaimTypes.GoogleUserId);

            var token = await dataStore.GetAsync<TokenResponse>(googleUserId);
            return new UserCredential(flow, googleUserId, token);
        }

    }
}
