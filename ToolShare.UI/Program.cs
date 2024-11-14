using System.Collections.Immutable;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using ToolShare.UI;
using ToolShare.UI.Identity;
using ToolShare.UI.Services;
using ToolShare.Data.Models;
using System.Reflection;
using ToolShare.UI.Identity.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// register the cookie handler
builder.Services.AddTransient<CookieHandler>();

// set up authorization
builder.Services.AddAuthorizationCore();

// register automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// register the custom authentication state provider
builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();

// register the account management interface
builder.Services.AddScoped(
    sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>());

// register the user info object
builder.Services.AddSingleton<UserInfo>();

// configure client for Pod Interactions
builder.Services.AddHttpClient<IPodsDataService, PodsDataService>(
    "PodsApi",
    opt => opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:5001"))
    .AddHttpMessageHandler<CookieHandler>();

// configure client for AppUser Interactions
 builder.Services.AddHttpClient<IUsersDataService, UsersDataService>(
     "AppUserApi",
     opt => opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:5001"))
     .AddHttpMessageHandler<CookieHandler>();

// configure client for auth interactions
builder.Services.AddHttpClient(
    "Auth",
    opt => opt.BaseAddress = new Uri(builder.Configuration["BackendUrl"] ?? "https://localhost:5001"))
    .AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();
