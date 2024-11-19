using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using Microsoft.VisualBasic;
using ToolShare.Data;
using ToolShare.Data.Models;
using YamlDotNet.Serialization;

namespace ToolShare.Api
{
    public class SeedData
    {
        private static readonly IEnumerable<SeedUser> seedUsers =
        [
            new SeedUser()
            {
                UserName = "tomsmith",
                NormalizedUserName = "TOMSMITH",
                Email = "smith@aol.com",
                NormalizedEmail = "SMITH@AOL.COM",
                FirstName = "Tom",
                LastName = "Smith",
                AboutMe = "Just a regular guy",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1730826604/FakeProfilePhoto2.jpg",
                RoleList = ["PodManager", "User"]
            },

            new SeedUser()
            {
                UserName = "fredjones",
                NormalizedUserName = "FREDJONES",
                Email = "jones@gmail.com",
                NormalizedEmail = "JONES@GMAIL.COM",
                FirstName = "Fred",
                LastName = "Jones",
                AboutMe = "Your neighborhood handyman",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1730826604/FakeProfilePhoto3.jpg",
                RoleList = ["PodManager", "User"]
            },

            new SeedUser()
            {
                UserName = "wendy123",
                NormalizedUserName = "WENDY123",
                Email = "wendy@hotmail.com",
                NormalizedEmail = "WENDY@HOTMAIL.COM",
                FirstName = "Wendy",
                LastName = "Jones",
                AboutMe = "Let's share!",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1730826605/FakeProfilePhoto7Female.jpg",
                RoleList = ["User"]
            },

            new SeedUser()
            {
                UserName = "teddybear5",
                NormalizedUserName = "TEDDYBEAR5",
                Email = "teddybear@hotmail.com",
                NormalizedEmail = "TEDDYBEAR@HOTMAIL.COM",
                FirstName = "Napoleon",
                LastName = "Dynamite",
                AboutMe = "I've got skillz",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1730826604/FakeProfilePhoto4.jpg",
                RoleList = ["User"]
            },

            new SeedUser()
            {
                UserName = "oliviasmith",
                NormalizedUserName = "OLIVIASMITH",
                Email = "olivia@protonmail.com",
                NormalizedEmail = "OLIVIA@PROTONMAIL.COM",
                FirstName = "Olivia",
                LastName = "Rodrigo",
                AboutMe = "Hey, you know me!",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1730826605/FakeProfilePhoto8Female.jpg",
                RoleList = ["User"]
            },

            new SeedUser()
            {
                UserName = "hungryhippo325",
                NormalizedUserName = "HUNGRYHIPPO325",
                Email = "dpg@aol.com",
                NormalizedEmail = "DPG@AOL.COM",
                FirstName = "Megan",
                LastName = "Livy",
                AboutMe = "Just a regular gal!",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1730826612/FakeProfilePhoto6Femal.jpg",
                RoleList = ["User"]
            },

            new SeedUser()
            {
                UserName = "jonjon",
                NormalizedUserName = "JONJON",
                Email = "jondonaldson@att.net",
                NormalizedEmail = "JONDONALDSON@ATT.NET",
                FirstName = "Jon",
                LastName = "Donaldson",
                AboutMe = "Everyone likes me!",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1730826604/FakeProfilePhoto1.jpg",
                RoleList = ["NoPodUser"]
            },

            new SeedUser()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "beckdp01@gmail.com",
                NormalizedEmail = "BECKDP01@GMAIL.COM",
                FirstName = "Admin",
                LastName = "Admin",
                AboutMe = "Administrative Account",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1730826604/FakeProfilePhoto2.jpg",
                RoleList = ["Administrator"]
            }

        ];
        
        public static async Task InitializeUsersAsync(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (await context.Users.AnyAsync())
            {
                return;
            }

            var userStore = new UserStore<AppUser>(context);
            var password = new PasswordHasher<AppUser>();

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

        public static async Task InitializePodsAsync(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (await context.Pods.AnyAsync())
            {
                return;
            }
            Pod[] seedPods =  
            [
                new Pod() { Name = "SmithPod"},
                new Pod() { Name = "JonesPod"},
            ];

            AppUser? tomsmithUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == "tomsmith");
            AppUser? teddybear5User = await context.Users.FirstOrDefaultAsync(u => u.UserName == "teddybear5");
            AppUser? oliviasmithUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == "oliviasmith");

            seedPods[0].PodMembers.Add(tomsmithUser);
            seedPods[0].podManager = tomsmithUser;

            seedPods[0].PodMembers.Add(teddybear5User);
            seedPods[0].PodMembers.Add(oliviasmithUser);

            AppUser? fredjonesUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == "fredjones");
            AppUser? wendy123User = await context.Users.FirstOrDefaultAsync(u => u.UserName == "wendy123");
            AppUser? hungryhippo325 = await context.Users.FirstOrDefaultAsync(u => u.UserName == "hungryhippo325");

            seedPods[1].PodMembers.Add(fredjonesUser);
            seedPods[1].podManager = fredjonesUser;

            seedPods[1].PodMembers.Add(wendy123User);
            seedPods[1].PodMembers.Add(hungryhippo325);

            await context.Pods.AddRangeAsync(seedPods);

            await context.SaveChangesAsync();
        }

        public static async Task InitializeToolsAsync (IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (await context.Tools.AnyAsync())
            {
                return;
            }

            Tool[] seedTools =  
            [
                new Tool() 
                {   
                    Name = "Honda Walk Behind Lawnmower",
                    Description = "A self-propelled walk behind lawnmower that works great.",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },

                new Tool() 
                {   
                    Name = "Scott's Push Reel Mower",
                    Description = "A push reel mower that is appropriate for smaller yards. Just need to keep blades sharp.",
                    BorrowingPeriodInDays = 14,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },

                new Tool() 
                {   
                    Name = "Ryobi Leaf Blower",
                    Description = "A cordless leaf blower.",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },

                new Tool() 
                {   
                    Name = "Dewalt Lead Blower",
                    Description = "A leaf blower. NOTE: Not battery operated; needs to be plugged in",
                    BorrowingPeriodInDays = 21,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },


                new Tool() 
                {   
                    Name = "Ladder (22 Ft.)",
                    Description = "A 22 ft. long aluminum ladder",
                    BorrowingPeriodInDays = 30,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },


                new Tool() 
                {   
                    Name = "Dewalt Brad Nailer",
                    Description = "18 Gauge; Cordless",
                    BorrowingPeriodInDays = 14,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },
                
                new Tool() 
                {   
                    Name = "Ridgid Framing Nailer",
                    Description = "8 nails per second Includes: Swivel Connect, No-Mar Pad, Oil, and Wrench",
                    BorrowingPeriodInDays = 14,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },
                
                new Tool() 
                {   
                    Name = "Tarps",
                    Description = "Tarps for painting. I have six of  them.",
                    BorrowingPeriodInDays = 180,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },
                
                new Tool() 
                {   
                    Name = "Graco Paint Sprayer",
                    Description = "Cordless, handheld, and airless sprayer.",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },

                    new Tool() 
                {   
                    Name = "Shopvac Wet/Dry Vacuum",
                    Description = "A bit old, but still works great",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },

                new Tool() 
                {   
                    Name = "Shovel",
                    Description = "It's just a shovel....",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },

                    new Tool() 
                {   
                    Name = "Hammer",
                    Description = "It's just a hammer...",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg"
                },

            ];

            // Get Users
            AppUser? tomsmithUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == "tomsmith");
            AppUser? teddybear5User = await context.Users.FirstOrDefaultAsync(u => u.UserName == "teddybear5");
            AppUser? oliviasmithUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == "oliviasmith");
            AppUser? fredjonesUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == "fredjones");
            AppUser? wendy123User = await context.Users.FirstOrDefaultAsync(u => u.UserName == "wendy123");
            AppUser? hungryhippo325User = await context.Users.FirstOrDefaultAsync(u => u.UserName == "hungryhippo325");

            // Add tools to users
            tomsmithUser.ToolsOwned.Add(seedTools[0]);
            tomsmithUser.ToolsOwned.Add(seedTools[1]);
            teddybear5User.ToolsOwned.Add(seedTools[2]);
            teddybear5User.ToolsOwned.Add(seedTools[3]);
            oliviasmithUser.ToolsOwned.Add(seedTools[4]);
            oliviasmithUser.ToolsOwned.Add(seedTools[5]);
            fredjonesUser.ToolsOwned.Add(seedTools[6]);
            fredjonesUser.ToolsOwned.Add(seedTools[7]);
            wendy123User.ToolsOwned.Add(seedTools[8]);
            wendy123User.ToolsOwned.Add(seedTools[9]);
            hungryhippo325User.ToolsOwned.Add(seedTools[10]);
            hungryhippo325User.ToolsOwned.Add(seedTools[11]);

            await context.Tools.AddRangeAsync(seedTools);

            await context.SaveChangesAsync();
        }

        private class SeedUser : AppUser
        {
            public string[]? RoleList { get; set; }
        }

    }
}