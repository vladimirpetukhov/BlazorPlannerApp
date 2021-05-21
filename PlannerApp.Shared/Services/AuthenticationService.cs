namespace PlannerApp.Shared.Services
{

    #region usings
    using AKSoftware.WebApi.Client;
    using PlannerApp.Shared.Models;
    using System.Threading.Tasks;
    #endregion

    public class AuthenticationService
    {
        #region fields
        private readonly string _baseUrl;
        #endregion

        //TODO: Implement own custom HttpClient Api
        ServiceClient client = new ServiceClient();

        #region ctor
        public AuthenticationService(string url)
        {
            _baseUrl = url;
        }
        #endregion

        #region methods
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterRequest request)
        {
            var response = await client.PostAsync<UserManagerResponse>($"{_baseUrl}/api/auth/register", request);
            return response.Result;
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginRequest request)
        {
            var response = await client.PostAsync<UserManagerResponse>($"{_baseUrl}/api/auth/login", request);
            return response.Result;
        }
        #endregion
    }
}
