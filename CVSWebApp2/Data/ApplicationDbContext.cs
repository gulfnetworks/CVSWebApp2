using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CVSWebApp2.Models;

namespace CVSWebApp2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, UserRoleIntPK, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            //builder.Entity<UserOutlet>().HasKey(uo => new { uo.Id, uo.OutletId });


            //builder.Entity<Outlet>().HasKey(c => new { c.OutletId, c.CompanyId, c.CountryId });

            //builder.Entity<Company>().HasKey(c => new { c.CompanyId, c.PaymentId });
            builder.Entity<ResolutionLog>().HasKey(a=> a.ResolutionLogId);

        }


        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Outlet> Outlets { get; set; }
        public DbSet<UserOutlet> UserOutlets { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Email> Emails { get; set; }

        public DbSet<CVSWebApp2.Models.ResolutionLog> ResolutionLogs { get; set; }

        public DbSet<CVSWebApp2.Models.Survey> Surveys { get; set; }

        public DbSet<CVSWebApp2.Models.LocationTimeZone> LocationTimeZones { get; set; }

        public DbSet<CVSWebApp2.Models.SocialMedia> SocialMedias { get; set; }

        public DbSet<CVSWebApp2.Models.MemberRole> MemberRoles { get; set; }

     
    }
}
