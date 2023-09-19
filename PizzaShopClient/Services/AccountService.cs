using PizzaShop.Shared;
using PizzaShopClient.Authentication;
using System.Net;
using System.Net.Http.Json;

namespace PizzaShopClient.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserSession?> Login(LoginRequest input)
        {
            var configuredPizza = await _httpClient.PostAsJsonAsync("Account", input);

            var result = await configuredPizza.Content.ReadFromJsonAsync<UserSession?>();

            if (configuredPizza.IsSuccessStatusCode)
            {
             return result;
            }
           
            return result;
        }

    }
}
