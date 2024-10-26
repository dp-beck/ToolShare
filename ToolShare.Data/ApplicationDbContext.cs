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
        public DbSet<JoinPodRequest> JoinPodRequests { get; set; }
        public DbSet<ShareRequest> ShareRequests{ get; set; }

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

            modelBuilder.Entity<Pod>()
                .HasMany(p => p.PodMembers)
                .WithOne(a => a.Pod)
                .HasForeignKey(a => a.PodId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}