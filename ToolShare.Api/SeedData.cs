using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToolShare.Data;
using YamlDotNet.Serialization;

namespace ToolShare.Api
{
    public class SeedData
    {
        private static readonly IEnumerable<SeedUser> seedUsers =
        [
            new SeedUser()
            {
                Email = "leela@contoso.com", 
                NormalizedEmail = "LEELA@CONTOSO.COM", 
                NormalizedUserName = "LEELA@CONTOSO.COM", 
                RoleList = [ "Administrator", "Manager" ], 
                UserName = "leela@contoso.com"          
            },

            new SeedUser()
            {
            Email = "harry@contoso.com",
            NormalizedEmail = "HARRY@CONTOSO.COM",
            NormalizedUserName = "HARRY@CONTOSO.COM",
            RoleList = [ "User" ],
            UserName = "harry@contoso.com"
            }
        ];

        // TO DO: Write the InitializeAsync Method
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.Users.Any())
            {
                return;
            }

            var userStore = new UserStore<AppUser>(context);
            var password = new PasswordHasher<AppUser>();

            using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = [ "Administrator", "Manager", "User" ];

            foreach (var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }   
            }

            using var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            foreach (var seedUser in seedUsers)
            {
                var hashed = password.HashPassword(seedUser, "Passw0rd!");
                seedUser.PasswordHash = hashed;
                await userStore.CreateAsync(seedUser);

                if (seedUser.Email is not null)
                {
                    var user = await userManager.FindByEmailAsync(seedUser.Email);

                    if (user is not null && seedUser.RoleList is not null)
                    {
                        await userManager.AddToRolesAsync(user, seedUser.RoleList);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
        private class SeedUser : AppUser
        {
            public string[]? RoleList { get; set; }
        }

    }
}