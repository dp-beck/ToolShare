﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToolShare.Data;

#nullable disable

namespace ToolShare.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ToolShare.Data.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("AboutMe")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PodId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProfilePhotoUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("PodId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ToolShare.Data.Models.JoinPodRequest", b =>
                {
                    b.Property<int>("JoinPodRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PodId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PodManagerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("JoinPodRequestId");

                    b.HasIndex("PodId");

                    b.HasIndex("PodManagerId");

                    b.HasIndex("RequestorId");

                    b.ToTable("JoinPodRequests");
                });

            modelBuilder.Entity("ToolShare.Data.Models.Pod", b =>
                {
                    b.Property<int>("PodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PodManagerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PodManagerId1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PodId");

                    b.HasIndex("PodManagerId1");

                    b.ToTable("Pods");
                });

            modelBuilder.Entity("ToolShare.Data.Models.ShareRequest", b =>
                {
                    b.Property<int>("ShareRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateRequested")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("IsShareRequested")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ToolOwnerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ToolRequestedToolId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ToolRequestorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ShareRequestId");

                    b.HasIndex("ToolOwnerId");

                    b.HasIndex("ToolRequestedToolId");

                    b.HasIndex("ToolRequestorId");

                    b.ToTable("ShareRequests");
                });

            modelBuilder.Entity("ToolShare.Data.Tool", b =>
                {
                    b.Property<int>("ToolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BorrowerId")
                        .HasColumnType("TEXT");

                    b.Property<int>("BorrowingPeriodInDays")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateDue")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ToolStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("ToolId");

                    b.HasIndex("BorrowerId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Tools");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ToolShare.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ToolShare.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToolShare.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ToolShare.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToolShare.Data.Models.AppUser", b =>
                {
                    b.HasOne("ToolShare.Data.Models.Pod", "Pod")
                        .WithMany("PodMembers")
                        .HasForeignKey("PodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Pod");
                });

            modelBuilder.Entity("ToolShare.Data.Models.JoinPodRequest", b =>
                {
                    b.HasOne("ToolShare.Data.Models.Pod", "RequestedPod")
                        .WithMany()
                        .HasForeignKey("PodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToolShare.Data.Models.AppUser", "PodManager")
                        .WithMany()
                        .HasForeignKey("PodManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToolShare.Data.Models.AppUser", "Requestor")
                        .WithMany()
                        .HasForeignKey("RequestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PodManager");

                    b.Navigation("RequestedPod");

                    b.Navigation("Requestor");
                });

            modelBuilder.Entity("ToolShare.Data.Models.Pod", b =>
                {
                    b.HasOne("ToolShare.Data.Models.AppUser", "PodManager")
                        .WithMany()
                        .HasForeignKey("PodManagerId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PodManager");
                });

            modelBuilder.Entity("ToolShare.Data.Models.ShareRequest", b =>
                {
                    b.HasOne("ToolShare.Data.Models.AppUser", "ToolOwner")
                        .WithMany()
                        .HasForeignKey("ToolOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToolShare.Data.Tool", "ToolRequested")
                        .WithMany()
                        .HasForeignKey("ToolRequestedToolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToolShare.Data.Models.AppUser", "ToolRequestor")
                        .WithMany()
                        .HasForeignKey("ToolRequestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ToolOwner");

                    b.Navigation("ToolRequested");

                    b.Navigation("ToolRequestor");
                });

            modelBuilder.Entity("ToolShare.Data.Tool", b =>
                {
                    b.HasOne("ToolShare.Data.Models.AppUser", "ToolBorrower")
                        .WithMany("ToolsBorrowed")
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ToolShare.Data.Models.AppUser", "ToolOwner")
                        .WithMany("ToolsOwned")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ToolBorrower");

                    b.Navigation("ToolOwner");
                });

            modelBuilder.Entity("ToolShare.Data.Models.AppUser", b =>
                {
                    b.Navigation("ToolsBorrowed");

                    b.Navigation("ToolsOwned");
                });

            modelBuilder.Entity("ToolShare.Data.Models.Pod", b =>
                {
                    b.Navigation("PodMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
