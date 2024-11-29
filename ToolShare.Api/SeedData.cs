using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToolShare.Data;
using ToolShare.Data.Models;

namespace ToolShare.Api
{
    public static class SeedData
    {
        private static readonly IEnumerable<SeedUser> SeedUsers =
        [
            new()
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

            new()
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

            new()
            {
                UserName = "wendy123",
                NormalizedUserName = "WENDY123",
                Email = "wendy@hotmail.com",
                NormalizedEmail = "WENDY@HOTMAIL.COM",
                FirstName = "Wendy",
                LastName = "Jones",
                AboutMe = "Let's share! I'm Fred's sister.",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1730826605/FakeProfilePhoto7Female.jpg",
                RoleList = ["User"]
            },
            
            new()
            {
                UserName = "janedoe",
                NormalizedUserName = "JANEDOE",
                Email = "jane.doe@example.com",
                NormalizedEmail = "JANE.DOE@EXAMPLE.COM",
                FirstName = "Jane",
                LastName = "Doe",
                AboutMe = "Enthusiastic book lover and traveler",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1731154587/blank-profile-picture-973460_1280_pxrhwm.png",
                RoleList = ["User"]
            },
            
            new()
            {
                UserName = "Sunshine",
                NormalizedUserName = "SUNSHINE",
                Email = "sara.ray@example.com",
                NormalizedEmail = "SARA.RAY@EXAMPLE.COM",
                FirstName = "Sara",
                LastName = "Ray",
                AboutMe = "Tech enthusiast and aspiring coder",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1731154587/blank-profile-picture-973460_1280_pxrhwm.png",
                RoleList = ["User"]
            },

            new()
            {
                UserName = "Bobbo",
                NormalizedUserName = "BOBBO",
                Email = "bob.jones@example.com",
                NormalizedEmail = "BOB.JONES@EXAMPLE.COM",
                FirstName = "Bob",
                LastName = "Jones",
                AboutMe = "Food blogger and amateur chef",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1731154587/blank-profile-picture-973460_1280_pxrhwm.png",
                RoleList = ["User"]
            },
            
            new()
            {
                UserName = "Big Mike",
                NormalizedUserName = "BIG MIKE",
                Email = "big.mike@example.com",
                NormalizedEmail = "big.mike@EXAMPLE.COM",
                FirstName = "Michael",
                LastName = "Moore",
                AboutMe = "I love documentaries and building bird houses.",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1731154587/blank-profile-picture-973460_1280_pxrhwm.png",
                RoleList = ["User"]
            },
            
            new()
            {
                UserName = "napoleon_dynamite",
                NormalizedUserName = "NAPOLEON_DYNAMITE",
                Email = "teddybear@hotmail.com",
                NormalizedEmail = "TEDDYBEAR@HOTMAIL.COM",
                FirstName = "Jon",
                LastName = "Heder",
                AboutMe = " A liger is my favorite animal because it's a mix between a lion and a tiger and was bred for its magical skills",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1730826604/FakeProfilePhoto4.jpg",
                RoleList = ["User"]
            },

            new()
            {
                UserName = "oliviasmith",
                NormalizedUserName = "OLIVIASMITH",
                Email = "olivia@protonmail.com",
                NormalizedEmail = "OLIVIA@PROTONMAIL.COM",
                FirstName = "Olivia",
                LastName = "Smith",
                AboutMe = "Hey, you know me!",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1730826605/FakeProfilePhoto8Female.jpg",
                RoleList = ["User"]
            },

            new()
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

            new()
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
            
            new()
            {
                UserName = "SharpScissors",
                NormalizedUserName = "SHARPSCISSORS",
                Email = "viggo@hotmail.net",
                NormalizedEmail = "VIGGO@HOTMAIL.NET",
                FirstName = "Viggo",
                LastName = "Wenders",
                AboutMe = "I have lots of tools and I am ready to share!",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1722303642/pejtc5ftiwtijro07dyq.jpg",
                RoleList = ["NoPodUser"]
            },
            
            new()
            {
                UserName = "coolhandluke",
                NormalizedUserName = "COOLHANDLUKE",
                Email = "luke@aol.com",
                NormalizedEmail = "LUKE@AOL.COM",
                FirstName = "Paul",
                LastName = "Newman",
                AboutMe = "I don't have very many tools; so, I'm hoping to just mooch off everyone else!",
                ProfilePhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1731154587/blank-profile-picture-973460_1280_pxrhwm.png",
                RoleList = ["NoPodUser"]
            },

            new()
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

            foreach (var seedUser in SeedUsers)
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
            
            // Get Users
            AppUser tomsmith = await context.Users.FirstOrDefaultAsync(u => u.UserName == "tomsmith") ?? throw new InvalidOperationException();
            AppUser napoleondynamite = await context.Users.FirstOrDefaultAsync(u => u.UserName == "napoleon_dynamite") ?? throw new InvalidOperationException();
            AppUser oliviasmith = await context.Users.FirstOrDefaultAsync(u => u.UserName == "oliviasmith") ?? throw new InvalidOperationException();
            AppUser janedoe = await context.Users.FirstOrDefaultAsync(u => u.UserName == "janedoe") ?? throw new InvalidOperationException();
            AppUser bigmike = await context.Users.FirstOrDefaultAsync(u => u.UserName == "Big Mike") ?? throw new InvalidOperationException();
       
            AppUser fredjones = await context.Users.FirstOrDefaultAsync(u => u.UserName == "fredjones") ?? throw new InvalidOperationException();
            AppUser wendy123 = await context.Users.FirstOrDefaultAsync(u => u.UserName == "wendy123") ?? throw new InvalidOperationException();
            AppUser sunshine = await context.Users.FirstOrDefaultAsync(u => u.UserName == "Sunshine") ?? throw new InvalidOperationException();
            AppUser bobbo = await context.Users.FirstOrDefaultAsync(u => u.UserName == "Bobbo") ?? throw new InvalidOperationException();
            AppUser hungryhippo325 = await context.Users.FirstOrDefaultAsync(u => u.UserName == "hungryhippo325") ?? throw new InvalidOperationException();

            // Create Seed Pods
            Pod[] seedPods =  
            [
                new ()
                {
                    Name = "Smith Pod",
                    PodManager = tomsmith,
                },
                new ()
                {
                    Name = "Jones Pod",
                    PodManager = fredjones,
                },
            ];
            
            // Add Pod Members
            seedPods[0].PodMembers.Add(tomsmith);
            seedPods[0].PodMembers.Add(napoleondynamite);
            seedPods[0].PodMembers.Add(oliviasmith);
            seedPods[0].PodMembers.Add(janedoe);
            seedPods[0].PodMembers.Add(bigmike);
            
            seedPods[1].PodMembers.Add(fredjones);
            seedPods[1].PodMembers.Add(wendy123);
            seedPods[1].PodMembers.Add(sunshine);
            seedPods[1].PodMembers.Add(bobbo);
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
            
            // Get Users
            AppUser tomsmith = await context.Users.FirstOrDefaultAsync(u => u.UserName == "tomsmith") ?? throw new InvalidOperationException();
            AppUser napoleondynamite = await context.Users.FirstOrDefaultAsync(u => u.UserName == "napoleon_dynamite") ?? throw new InvalidOperationException();
            AppUser oliviasmith = await context.Users.FirstOrDefaultAsync(u => u.UserName == "oliviasmith") ?? throw new InvalidOperationException();
            AppUser janedoe = await context.Users.FirstOrDefaultAsync(u => u.UserName == "janedoe") ?? throw new InvalidOperationException();
            AppUser bigmike = await context.Users.FirstOrDefaultAsync(u => u.UserName == "Big Mike") ?? throw new InvalidOperationException();
       
            AppUser fredjones = await context.Users.FirstOrDefaultAsync(u => u.UserName == "fredjones") ?? throw new InvalidOperationException();
            AppUser wendy123 = await context.Users.FirstOrDefaultAsync(u => u.UserName == "wendy123") ?? throw new InvalidOperationException();
            AppUser sunshine = await context.Users.FirstOrDefaultAsync(u => u.UserName == "Sunshine") ?? throw new InvalidOperationException();
            AppUser bobbo = await context.Users.FirstOrDefaultAsync(u => u.UserName == "Bobbo") ?? throw new InvalidOperationException();
            AppUser hungryhippo325 = await context.Users.FirstOrDefaultAsync(u => u.UserName == "hungryhippo325") ?? throw new InvalidOperationException();

            
            Tool[] seedTools =  
            [
                new () 
                {   
                    Name = "Honda Walk Behind Lawnmower",
                    Description = "A self-propelled walk behind lawnmower that works great.",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732847991/cub-cadet-gas-self-propelled-lawn-mowers-cc800-64_1000_agqcge.jpg",
                    ToolOwner = tomsmith,
                    ToolStatus = ToolStatus.Borrowed,
                    ToolBorrower = napoleondynamite
                },

                new () 
                {   
                    Name = "Scott's Push Reel Mower",
                    Description = "A push reel mower that is appropriate for smaller yards. Just need to keep blades sharp.",
                    BorrowingPeriodInDays = 14,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848042/scotts-reel-lawn-mowers-415-16s-4f_600_mm0n02.jpg",
                    ToolOwner = tomsmith
                },

                new () 
                {   
                    Name = "Ryobi Leaf Blower",
                    Description = "A cordless leaf blower.",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848104/146b0a4304d448c5b04ed50630097831_t2pp5w.jpg",
                    ToolOwner = napoleondynamite,
                    ToolStatus = ToolStatus.Borrowed,
                    ToolBorrower = oliviasmith
                },

                new () 
                {   
                    Name = "Dewalt Leaf Blower",
                    Description = "A leaf blower. NOTE: Not battery operated; needs to be plugged in",
                    BorrowingPeriodInDays = 21,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848148/dewalt-cordless-leaf-blowers-dcbl722p1-64_600_lduuii.jpg",
                    ToolOwner = napoleondynamite
                },

                new () 
                {   
                    Name = "Christmas Light Hanger",
                    Description = "It's used to hang your lights on your roof, so you don't have to climb up there.",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848427/90603A_1000x1000_ptlsns.jpg",
                    ToolOwner = janedoe,
                    ToolStatus = ToolStatus.Requested,
                    ToolRequester = napoleondynamite
                },
                
                new () 
                {   
                    Name = "Rubber Mallet",
                    Description = "Very handy when driving stakes into the ground.",
                    BorrowingPeriodInDays = 21,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732729454/screwdriver-1294338_1280_e5qlme.png",
                    ToolOwner = janedoe,
                    ToolStatus = ToolStatus.ReturnPending,
                    ToolBorrower = bigmike
                },
                
                new () 
                {   
                    Name = "Portable boom box",
                    Description = "Perfect for really loud parties.",
                    BorrowingPeriodInDays = 23,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848505/boombox2-600x600_grande_xlqbmk.jpg",
                    ToolOwner = bigmike
                },
                
                new () 
                {   
                    Name = "Soldering Iron",
                    Description = "I don't know? In case you got some microchips in need of soldering....",
                    BorrowingPeriodInDays = 200,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732729454/screwdriver-1294338_1280_e5qlme.png",
                    ToolOwner = bigmike
                },
                
                new () 
                {   
                    Name = "Ladder (22 Ft.)",
                    Description = "A 22 ft. long aluminum ladder",
                    BorrowingPeriodInDays = 30,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848531/werner-step-ladders-nxt1a06-64_600_bftcg1.jpg",
                    ToolOwner = oliviasmith
                },
                
                new () 
                {   
                    Name = "Dewalt Brad Nailer",
                    Description = "18 Gauge; Cordless",
                    BorrowingPeriodInDays = 14,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848568/dewalt-brad-nailers-dwfp12233-64_1000_ky37mo.jpg",
                    ToolOwner = oliviasmith,
                    ToolStatus = ToolStatus.Borrowed,
                    ToolBorrower = bigmike
                },
                
                new () 
                {   
                    Name = "Ridgid Framing Nailer",
                    Description = "8 nails per second Includes: Swivel Connect, No-Mar Pad, Oil, and Wrench",
                    BorrowingPeriodInDays = 14,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848602/2f3d408f-42a9-4c3b-b4d6-46aaa9a30fcd_wnryqj.jpg",
                    ToolOwner = fredjones,
                    ToolStatus = ToolStatus.ReturnPending,
                    ToolBorrower = wendy123
                },
                
                new () 
                {   
                    Name = "Tarps",
                    Description = "Tarps for painting. I have six of  them.",
                    BorrowingPeriodInDays = 180,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732729454/screwdriver-1294338_1280_e5qlme.png",
                    ToolOwner = fredjones,
                    ToolStatus = ToolStatus.Requested,
                    ToolRequester = sunshine
                },
                
                new () 
                {   
                    Name = "Graco Paint Sprayer",
                    Description = "Cordless, handheld, and airless sprayer.",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848734/61fjWkGxtGL_qukyxw.jpg",
                    ToolOwner = wendy123
                },

                new () 
                {   
                    Name = "Shopvac Wet/Dry Vacuum",
                    Description = "A bit old, but still works great",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848775/5989305_Main_1200x_eowdva.jpg",
                    ToolOwner = wendy123,
                    ToolStatus = ToolStatus.Borrowed,
                    ToolBorrower = bobbo
                },
                
                new () 
                {   
                    Name = "Sprinkler",
                    Description = "Keep your lawn nice and green during the dry times.",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848808/XT45360M_IMG_IU2_RGB_1500_lrztii.jpg",
                    ToolOwner = sunshine,
                    ToolStatus = ToolStatus.Borrowed,
                    ToolBorrower = hungryhippo325
                },

                new () 
                {   
                    Name = "Extra Christmas Lights",
                    Description = "A whole bunch of extra christmas lights I don't use. Go ahead and borrow them",
                    BorrowingPeriodInDays = 250,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732729454/screwdriver-1294338_1280_e5qlme.png",
                    ToolOwner = sunshine
                },
                
                new () 
                {   
                    Name = "Chainsaw",
                    Description = "Good for like, you know, cutting trees down.",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848837/71lS6YPQqVL_ebayrd.jpg",
                    ToolOwner = bobbo
                },

                new () 
                {   
                    Name = "Jackhammer",
                    Description = "Good for like, you know, breaking up concrete",
                    BorrowingPeriodInDays = 21,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732848859/thumb-electric-jack-hammers_fu2ilr.jpg",
                    ToolOwner = bobbo,
                    ToolStatus = ToolStatus.ReturnPending,
                    ToolBorrower = hungryhippo325
                },
                
                new () 
                {   
                    Name = "Shovel",
                    Description = "It's just a shovel....",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732729454/screwdriver-1294338_1280_e5qlme.png",
                    ToolOwner = hungryhippo325
                },

                new () 
                {   
                    Name = "Hammer",
                    Description = "It's just a hammer...",
                    BorrowingPeriodInDays = 7,
                    ToolPhotoUrl = "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1731874629/naomi-o-hare-ziu7At0z4CE-unsplash_bzzfdg.jpg",
                    ToolOwner = hungryhippo325
                },

            ];
            
            await context.Tools.AddRangeAsync(seedTools);

            await context.SaveChangesAsync();
        }

        private class SeedUser : AppUser
        {
            public string[]? RoleList { get; set; }
        }

    }
}