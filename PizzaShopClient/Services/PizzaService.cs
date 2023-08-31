

using PizzaShop.Shared;
using PizzaShopClient.Pages;
using System.Net.Http.Json;

namespace PizzaShopClient.Services
{
    public class PizzaService: IPizzaService
    {
        private readonly HttpClient _httpClient;

        public PizzaService(HttpClient httpClient)  
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponse<List<PizzaSpecial>>> GetListPizzaSpecials()
        {
            var specials = await _httpClient.GetFromJsonAsync<BaseResponse<List<PizzaSpecial>>>("Specials/GetList/specials");

            return specials;
        }
        public async Task<BaseResponse<List<Topping>>> GetListTopping()
        {
            var toppings = await _httpClient.GetFromJsonAsync<BaseResponse<List<Topping>>>("Toppings/GetToppings");

            return toppings;
        }

        public async Task<Order> PlaceOrder(Order input)
        {
            var configuredPizza = await _httpClient.PostAsJsonAsync("Oders/PlaceOrder", input);

            var order = await configuredPizza.Content.ReadFromJsonAsync<Order>();
            if (order != null)
            {
                return order;
            }
            else
            {
                return new Order();
            }
        }
        public async Task<BaseResponse<List<OrderWithStatus>>> GetOrders()
        {
            var order = await _httpClient.GetFromJsonAsync<BaseResponse<List<OrderWithStatus>>>("Oders/GetOrders");

            return order;
        }

        public async Task<BaseResponse<OrderWithStatus>> GetOrderWithStatus(int orderId)
        {
            var order = await _httpClient.GetFromJsonAsync<BaseResponse<OrderWithStatus>>("Oders/GetOrderWithStatus/" + orderId);

            return order;
        }
    

    }
}
