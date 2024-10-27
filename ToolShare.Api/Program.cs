using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

// Add NSwag Services
// TO DO: What are Nswag services?
builder.Services.AddOpenApiDocument();

builder.Services.AddControllers().AddJsonOptions(x => 
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IToolsRepository, ToolsRepository>();
builder.Services.AddScoped<IPodsRepository, PodsRepository>();

var app = builder.Build();

// Coonfigure Roles for Authorization
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync("PodManager"))
        await roleManager.CreateAsync(new IdentityRole("PodManager"));
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Seed the Database
    // await using var scope = app.Services.CreateAsyncScope();
    // await SeedData.InitializeAsync(scope.ServiceProvider);
    
    app.UseOpenApi();
    app.UseSwaggerUI();
}

app.UseCors("wasm");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// provide an endpoint for user roles
app.MapGet("/roles", (ClaimsPrincipal user) =>
{
    if (user.Identity is not null && user.Identity.IsAuthenticated)
    {
        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType)
            .Select(c => 
                new
                {
                    c.Issuer, 
                    c.OriginalIssuer, 
                    c.Type, 
                    c.Value, 
                    c.ValueType
                });

        return TypedResults.Json(roles);
    }

    return Results.Unauthorized();
}).RequireAuthorization();

app.MapControllers();

app.Run();
