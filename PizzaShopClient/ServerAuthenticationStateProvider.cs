using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Security.Claims;

namespace PizzaShopClient
{
    public class ServerAuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public ServerAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    var userInfo = await _httpClient.GetJsonAsync<UserInfo>("user");

        //    var identity = userInfo.IsAuthenticated
        //        ? new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userInfo.Name) }, "serverauth")
        //        : new ClaimsIdentity();

        //    return new AuthenticationState(new ClaimsPrincipal(identity));
        //}
    }
}
