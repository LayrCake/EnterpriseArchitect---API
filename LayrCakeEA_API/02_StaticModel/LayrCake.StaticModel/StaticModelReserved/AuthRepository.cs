using LayrCake.StaticModel.DataVisualiserServiceReference;
using _StaticModel.Repositories.Abstract;

namespace LayrCake.StaticModel.StaticModelReserved
{
    public class AuthRepository : RepositoryBase, IAuthRepository
    {
        public string GetToken()
        {
            var request = new TokenRequest(); //.Prepare();
            request.RequestId = RequestHelper.RequestId;
            request.ClientTag = RequestHelper.ClientTag;

            var response = Client.GetToken(request);

            Correlate(request, response);

            return response.AccessToken;
        }

        public bool Login(string username, string password)
        {
            var request = new LoginRequest().Prepare();
            request.UserName = username;
            request.Password = password;

            var response = Client.Login(request);

            Correlate(request, response);

            return response.Acknowledge == AcknowledgeType.Success;
        }

        public bool Logout()
        {
            var request = new LogoutRequest().Prepare();
            var response = Client.Logout(request);

            Correlate(request, response);

            return response.Acknowledge == AcknowledgeType.Success;
        }
    }
}