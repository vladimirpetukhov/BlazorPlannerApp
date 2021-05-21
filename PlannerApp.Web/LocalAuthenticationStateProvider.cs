namespace PlannerApp.Web
{

    #region usings
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;
    using PlannerApp.Web.Models;
    using System.Security.Claims;
    using System.Threading.Tasks;
    #endregion

    public class LocalAuthenticationStateProvider : AuthenticationStateProvider
    {
        #region fields
        private readonly ILocalStorageService _storageService;
        #endregion

        #region ctor
        public LocalAuthenticationStateProvider(ILocalStorageService storageService)
        {
            _storageService = storageService;
        }
        #endregion

        #region methods
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _storageService.ContainKeyAsync("User"))
            {
                var userInfo = await _storageService.GetItemAsync<LocalUserInfo>("User");

                var claims = new[]
                {
                    new Claim("Email", userInfo.Email),
                    new Claim("FirstName", userInfo.FirstName),
                    new Claim("LastName", userInfo.LastName),
                    new Claim("AccessToken", userInfo.AccessToken),
                    new Claim(ClaimTypes.NameIdentifier, userInfo.Id),
                };

                var identity = new ClaimsIdentity(claims, "BearerToken");
                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }

        public async Task LogoutAsync()
        {
            await _storageService.RemoveItemAsync("User");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }
        #endregion

    }
}
