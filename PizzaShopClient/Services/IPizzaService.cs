using PizzaShop.Shared;
using PizzaShopClient.Pages;

namespace PizzaShopClient.Services
{
    public interface IPizzaService
    {
        Task<BaseResponse<List<PizzaSpecial>>> GetListPizzaSpecials();
        Task<BaseResponse<List<Topping>>> GetListTopping();
        Task<Order> PlaceOrder(Order input);
        Task<BaseResponse<List<OrderWithStatus>>> GetOrders();
        Task<BaseResponse<OrderWithStatus>> GetOrderWithStatus(int orderId);

    }
}
