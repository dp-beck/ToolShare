using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToolShare.Data.Models;

namespace ToolShare.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Pod> Pods { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.ToolsOwned)
                .WithOne(t => t.ToolOwner)
                .HasForeignKey(t => t.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.ToolsBorrowed)
                .WithOne(t => t.ToolBorrower)
                .HasForeignKey(t => t.BorrowerId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.ToolsRequested)
                .WithOne(t => t.ToolRequester)
                .HasForeignKey(t => t.RequesterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .Property(a => a.ProfilePhotoUrl)
                .HasDefaultValue("https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1731154587/blank-profile-picture-973460_1280_pxrhwm.png");

            modelBuilder.Entity<Pod>()
                .HasMany(p => p.PodMembers)
                .WithOne(a => a.PodJoined)
                .HasForeignKey(a => a.PodJoinedId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pod>()
                .HasOne(p => p.PodManager)
                .WithOne(pm => pm.PodManaged)
                .HasForeignKey<AppUser>(pm => pm.PodManagedId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Pod>()
                .HasIndex(p => p.Name)
                .IsUnique();
            
            modelBuilder.Entity<Tool>()
                .Property(a => a.ToolPhotoUrl)
                .HasDefaultValue("https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732729454/screwdriver-1294338_1280_e5qlme.png");
        }
    }
}