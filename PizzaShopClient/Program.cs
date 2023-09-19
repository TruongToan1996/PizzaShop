using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PizzaShop.Shared;
using PizzaShopClient;
using PizzaShopClient.Authentication;
using PizzaShopClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//builder.Services.AddScoped<ProtectedSessionStorage>();



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5206/api/") });
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<OrderState>(); //Dang ky 1 cai services
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();



await builder.Build().RunAsync();
