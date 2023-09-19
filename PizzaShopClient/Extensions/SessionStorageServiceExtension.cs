using Blazored.SessionStorage;
using System.Text.Json;
using System.Text;

namespace PizzaShopClient.Extensions
{
    public static class SessionStorageServiceExtension
    {
        public static async Task SaveItemEncryptedAsync<T>(this ISessionStorageService sessionStorageService, string key, T item)

        {
            var itemJson = JsonSerializer.Serialize(item); // Tạo chuỗi Json sử dụng JsonSerializer
            var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson); // Chuyển đổi Json thành byte
            var base64Json = Convert.ToBase64String(itemJsonBytes); // Chuyển đổi byte → Base64String
            await sessionStorageService.SetItemAsync(key, base64Json); // Lưu trong bộ lưu trữ phiên

        }
        public static async Task<T> ReadEncryptedItemAsync<T>(this ISessionStorageService sessionStorageService, string key)
        {
            var base64Json = await sessionStorageService.GetItemAsync<string>(key);
            var itemJsonBytes = Convert.FromBase64String(base64Json);
            var itemJson = Encoding.UTF8.GetString(itemJsonBytes);
            var item = JsonSerializer.Deserialize<T>(itemJson); // Deserialize json cho đối tượng bằng cách sử dụng jsonSerializer trả về đối tượng
            return item;
        }
    }
}

