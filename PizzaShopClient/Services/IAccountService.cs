using PizzaShop.Shared;

namespace PizzaShopClient.Services
{
    public interface IAccountService
    {
        Task<UserSession?> Login(LoginRequest input);
    }
}
