using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PizzaShop.Shared;
using PizzaShopClient.Extensions;
using System.Security.Claims;

namespace PizzaShopClient.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _sessionStorage.ReadEncryptedItemAsync<UserSession>("UserSession"); // Đọc UserSession từ Session Storage
                if (userSession == null)

                    return await Task.FromResult(new AuthenticationState(_anonymous)); // Trả về trạng thái xác thực của 1 người dùng ẩn danh
                var claimsPrincial = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> //Nếu tồn tại UserSession xác nhận 2 quyền sở hữu mới
             {
                 new Claim(ClaimTypes.Name, userSession.UserName),
                 new Claim(ClaimTypes.Role, userSession.Role)
             }, "JwtAuth")); // Gtri chuỗi JwtAuth cung cấp cho loại Xác thực ( thiếu thì ứng dụng coi ng dùng là ẩn danh)
                return await Task.FromResult(new AuthenticationState(claimsPrincial)); // Trả về trạng thái xác thực quyền sở hữu mới dc tạo ra
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous)); // Trả về trạng thái xác thực của người dùng ẩn danh
            }
        }
        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal; // Khai báo đối tượng
            if (userSession != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userSession.UserName),
                new Claim(ClaimTypes.Role, userSession.Role)
            }));
                userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.ExpiresIn); // kiểm tra mã thông báo hết hạn
                await _sessionStorage.SaveItemEncryptedAsync("UserSession", userSession); // Save ng dùng vào bộ nhớ phiên
            }
            else
            {
                claimsPrincipal = _anonymous; // claimsPrincipal ẩn danh
                await _sessionStorage.RemoveItemAsync("UserSession"); // Đăng xuất thì Remove khỏi bộ nhớ phiên 
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal))); // thông báo cho blazor sự thay đổi trạng thái xác thực
        }
        public async Task<string> GetToken()
        {
            var result = string.Empty; // Mã thông báo API
            try
            {
                var userSession = await _sessionStorage.ReadEncryptedItemAsync<UserSession>("UserSession"); // check xem mã hết hạn hay chưa
                if (userSession != null && DateTime.Now < userSession.ExpiryTimeStamp)  // SSanh time now vs ExpiryTimeStamp
                    result = userSession.Token;
            }
            catch
            {

            }
            return result;
        }
    }
}
