﻿// <auto-generated />
using CVSWebApp2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CVSWebApp2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180205091342_migration1")]
    partial class migration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CVSWebApp2.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("Active");

                    b.Property<int>("CompanyId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("ConfirmPassword");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<int>("MemberRoleId");

                    b.Property<string>("MobileNo");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<bool>("RememberMe");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("MemberRoleId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CVSWebApp2.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyAddress");

                    b.Property<string>("CompanyCode");

                    b.Property<string>("CompanyContact");

                    b.Property<string>("CompanyLogoUrl");

                    b.Property<string>("CompanyName");

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("ExpiryDate");

                    b.Property<int>("LocationTimeZoneId");

                    b.HasKey("CompanyId");

                    b.HasIndex("CountryId");

                    b.HasIndex("LocationTimeZoneId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CVSWebApp2.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CVSWebApp2.Models.Email", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AutoEmail");

                    b.Property<string>("CC");

                    b.Property<int>("CompanyId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Folder");

                    b.Property<string>("From");

                    b.Property<bool>("IsRead");

                    b.Property<string>("Subject");

                    b.Property<string>("To");

                    b.HasKey("EmailId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("CVSWebApp2.Models.LocationTimeZone", b =>
                {
                    b.Property<int>("LocationTimeZoneId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("TimeZoneValue");

                    b.HasKey("LocationTimeZoneId");

                    b.ToTable("LocationTimeZones");
                });

            modelBuilder.Entity("CVSWebApp2.Models.MemberRole", b =>
                {
                    b.Property<int>("MemberRoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MemberRoleName");

                    b.HasKey("MemberRoleId");

                    b.ToTable("MemberRoles");
                });

            modelBuilder.Entity("CVSWebApp2.Models.Outlet", b =>
                {
                    b.Property<int>("OutletId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<int>("CountryId");

                    b.Property<string>("OutletAddress");

                    b.Property<string>("OutletName");

                    b.HasKey("OutletId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CountryId");

                    b.ToTable("Outlets");
                });

            modelBuilder.Entity("CVSWebApp2.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Amount");

                    b.Property<int>("CompanyId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Status");

                    b.Property<string>("Type");

                    b.HasKey("PaymentId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("CVSWebApp2.Models.ResolutionLog", b =>
                {
                    b.Property<int>("ResolutionLogId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApplicationUserId");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("ResolutionDetails");

                    b.Property<string>("Status");

                    b.Property<int>("SurveyId");

                    b.Property<int>("UpdaterId");

                    b.HasKey("ResolutionLogId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("SurveyId");

                    b.ToTable("ResolutionLogs");
                });

            modelBuilder.Entity("CVSWebApp2.Models.SocialMedia", b =>
                {
                    b.Property<int>("SocialMediaId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<int>("CompanyId");

                    b.Property<string>("JSONSettings");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("SocialMediaId");

                    b.HasIndex("CompanyId");

                    b.ToTable("SocialMedias");
                });

            modelBuilder.Entity("CVSWebApp2.Models.Survey", b =>
                {
                    b.Property<int>("SurveyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<string>("AmbienceComment");

                    b.Property<int>("AmbienceRate");

                    b.Property<string>("CheckNo");

                    b.Property<string>("Customer");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Email");

                    b.Property<int>("LastVisit");

                    b.Property<string>("LastVisitComment");

                    b.Property<int>("ManagerId");

                    b.Property<string>("MobileNo");

                    b.Property<int>("OutletId");

                    b.Property<string>("QualityComment");

                    b.Property<int>("QualityRate");

                    b.Property<string>("RecommendImprovements");

                    b.Property<string>("RecommendPoorArea");

                    b.Property<int>("RecommendRate");

                    b.Property<string>("RecommendSuggestions");

                    b.Property<string>("ServiceComment");

                    b.Property<int>("ServiceRate");

                    b.Property<int>("StaffId");

                    b.Property<string>("Status");

                    b.Property<string>("TableNo");

                    b.Property<string>("ValueComment");

                    b.Property<int>("ValueRate");

                    b.HasKey("SurveyId");

                    b.HasIndex("ManagerId");

                    b.HasIndex("OutletId");

                    b.HasIndex("StaffId");

                    b.ToTable("Surveys");
                });

            modelBuilder.Entity("CVSWebApp2.Models.UserOutlet", b =>
                {
                    b.Property<int>("UserOutletId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApplicationUserId");

                    b.Property<bool>("DefaultOutlet");

                    b.Property<int>("Id");

                    b.Property<int>("OutletId");

                    b.HasKey("UserOutletId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("OutletId");

                    b.ToTable("UserOutlets");
                });

            modelBuilder.Entity("CVSWebApp2.Models.UserRoleIntPK", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CVSWebApp2.Models.ApplicationUser", b =>
                {
                    b.HasOne("CVSWebApp2.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CVSWebApp2.Models.MemberRole", "MemberRole")
                        .WithMany()
                        .HasForeignKey("MemberRoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CVSWebApp2.Models.Company", b =>
                {
                    b.HasOne("CVSWebApp2.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CVSWebApp2.Models.LocationTimeZone", "LocationTimeZone")
                        .WithMany()
                        .HasForeignKey("LocationTimeZoneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CVSWebApp2.Models.Email", b =>
                {
                    b.HasOne("CVSWebApp2.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CVSWebApp2.Models.Outlet", b =>
                {
                    b.HasOne("CVSWebApp2.Models.Company", "Company")
                        .WithMany("Outlets")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CVSWebApp2.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CVSWebApp2.Models.Payment", b =>
                {
                    b.HasOne("CVSWebApp2.Models.Company", "Company")
                        .WithMany("Payments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CVSWebApp2.Models.ResolutionLog", b =>
                {
                    b.HasOne("CVSWebApp2.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CVSWebApp2.Models.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CVSWebApp2.Models.SocialMedia", b =>
                {
                    b.HasOne("CVSWebApp2.Models.Company", "Company")
                        .WithMany("SocialMedias")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CVSWebApp2.Models.Survey", b =>
                {
                    b.HasOne("CVSWebApp2.Models.ApplicationUser", "UserManager")
                        .WithMany("SurveyManagers")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CVSWebApp2.Models.Outlet", "Outlet")
                        .WithMany()
                        .HasForeignKey("OutletId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CVSWebApp2.Models.ApplicationUser", "UserStaff")
                        .WithMany("SurveyStaffs")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CVSWebApp2.Models.UserOutlet", b =>
                {
                    b.HasOne("CVSWebApp2.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("CVSWebApp2.Models.Outlet", "Outlet")
                        .WithMany("UserOutlets")
                        .HasForeignKey("OutletId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("CVSWebApp2.Models.UserRoleIntPK")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("CVSWebApp2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("CVSWebApp2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("CVSWebApp2.Models.UserRoleIntPK")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CVSWebApp2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("CVSWebApp2.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
