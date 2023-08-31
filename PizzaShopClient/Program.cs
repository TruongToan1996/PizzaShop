using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PizzaShop.Shared;
using PizzaShopClient;
using PizzaShopClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7062/api/") });
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddScoped<OrderState>(); //Dang ky 1 cai services

await builder.Build().RunAsync();
