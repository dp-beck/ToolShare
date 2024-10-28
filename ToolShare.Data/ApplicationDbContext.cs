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

            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.JoinPodRequestsReceived)
                .WithOne(j => j.Receiver)
                .HasForeignKey(j => j.ReceiverId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pod>()
                .HasMany(p => p.PodMembers)
                .WithOne(a => a.PodJoined)
                .HasForeignKey(a => a.PodJoinedId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pod>()
                .HasOne(p => p.podManager)
                .WithOne(pm => pm.PodManaged)
                .HasForeignKey<AppUser>(pm => pm.PodManagedId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<JoinPodRequest>()
                .HasOne(j => j.Requester)
                .WithOne(r => r.joinPodRequestSent)
                .HasForeignKey<AppUser>(r => r.joinPodRequestsentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JoinPodRequest>()
                .HasOne(j => j.podRequested)
                .WithMany(p => p.requestsToJoin)
                .HasForeignKey(j => j.podRequestedId)
                 .OnDelete(DeleteBehavior.Cascade);
        }

    }
}