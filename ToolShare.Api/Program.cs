using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToolShare.Api;
using ToolShare.Data;
using ToolShare.Data.Models;
using ToolShare.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = 
    builder.Configuration.GetConnectionString("ToolShare") ?? "Data Source=../ToolShare.Data/ToolShare.db";

// Add the Database
builder.Services.AddSqlite<ApplicationDbContext>(connectionString);

// Add Authentication with Cookies
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

// Add Configurable Authorization
builder.Services.AddAuthorizationBuilder();

// Add Identity and Opt in to API Endpoints
builder.Services.AddIdentityCore<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/users/login";
    options.Events.OnRedirectToLogin = context => 
    {
        context.Response.StatusCode = 401; // Unauthorized
        return Task.CompletedTask;
    };
});
    

// Add a CORS Policy
builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:5001", 
            builder.Configuration["FrontendUrl"] ?? "https://localhost:5002"])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApiDocument();

builder.Services.AddControllers().AddJsonOptions(x => 
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
    
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IToolsRepository, ToolsRepository>();
builder.Services.AddScoped<IPodsRepository, PodsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await using (var scope = app.Services.CreateAsyncScope())
    {
        // Ensure Local Database Is Created and Migrations Applied 
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync();
        
        // Configure Roles for Authorization
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = ["NoPodUser", "User", "PodManager", "Administrator"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
        
        // See Local Database
        await SeedData.InitializeUsersAsync(scope.ServiceProvider);
        await SeedData.InitializePodsAsync(scope.ServiceProvider);
        await SeedData.InitializeToolsAsync(scope.ServiceProvider);
        
    }
    
    app.UseOpenApi();
    app.UseSwaggerUI();
}

// Configure Roles for Authorization
// using (var scope = app.Services.CreateScope())
// {
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//     string[] roles = ["NoPodUser", "User", "PodManager", "Administrator"];
//
//     foreach (var role in roles)
//     {
//         if (!await roleManager.RoleExistsAsync(role))
//             await roleManager.CreateAsync(new IdentityRole(role));
//     }
// }

app.UseCors("wasm");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
