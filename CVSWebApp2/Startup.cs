using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CVSWebApp2.Data;
using CVSWebApp2.Models;
using CVSWebApp2.Services;
using Microsoft.AspNetCore.Rewrite;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Globalization;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace CVSWebApp2
{

    public class CompaniesRedirectRule : IRule
    {
        private readonly string[] matchPaths;
        private readonly PathString newPath;

        public CompaniesRedirectRule(string[] matchPaths, string newPath)
        {
            this.matchPaths = matchPaths;
            this.newPath = new PathString(newPath);
        }

        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;


            // Add null company
            //var emptyCompany = new Company();
            //emptyCompany.CompanyCode = "";
            //emptyCompany.CompanyName = "";

            //context.HttpContext.Session.SetObjectAsJson("CompanyName", emptyCompany);

            //context.HttpContext.Session.SetString("CompanyName", ""); // store byte array


    


            // if already redirected, skip
            if (request.Path.StartsWithSegments(new PathString(this.newPath)))
            {
                return;
            }




            var aCompany = new List<string>();


            var resultTable = new RawSQLDataProvider().Execute("SELECT [CompanyCode] FROM [Companies]");


            foreach (DataRow row in resultTable.Rows)
            {
                aCompany.Add(row["CompanyCode"].ToString());
            }
    

            if (aCompany.Contains(request.Path.Value.Replace("/","")))
            {
                var newLocation = $"{this.newPath}{request.Path.Value.Replace("/", "")}";

                var response = context.HttpContext.Response;
                response.StatusCode = StatusCodes.Status302Found;
                context.Result = RuleResult.EndResponse;
                response.Headers[HeaderNames.Location] = newLocation;
            }


        }
    }


    public static class TextCase
    {
        public static string ToProperCase(this string Text)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(Text.ToLower());    
        }
    }


    public static class CurrentSession
    {
        public static Company GetCurrentCompanyByCode(ApplicationDbContext context, string CompanyCode)
        {

                return context.Companies.Where(a => a.CompanyCode == CompanyCode).FirstOrDefault();
        }

    }

    public static class SeedData
    {
        // SEED DATA
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                context.Database.EnsureCreated();

                // SEED DATA FOR TIMEZONE TABLE
                if (!context.LocationTimeZones.Any())
                {
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Dateline Standard Time", TimeZoneValue = "(GMT-12:00) International Date Line West" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Samoa Standard Time", TimeZoneValue = "(GMT-11:00) Midway Island, Samoa" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Hawaiian Standard Time", TimeZoneValue = "(GMT-10:00) Hawaii" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Alaskan Standard Time", TimeZoneValue = "(GMT-09:00) Alaska" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Pacific Standard Time", TimeZoneValue = "(GMT-08:00) Pacific Time (US and Canada); Tijuana" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Mountain Standard Time", TimeZoneValue = "(GMT-07:00) Mountain Time (US and Canada)" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Mexico Standard Time 2", TimeZoneValue = "(GMT-07:00) Chihuahua, La Paz, Mazatlan" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "U.S. Mountain Standard Time", TimeZoneValue = "(GMT-07:00) Arizona" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Central Standard Time", TimeZoneValue = "(GMT-06:00) Central Time (US and Canada" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Canada Central Standard Time", TimeZoneValue = "(GMT-06:00) Saskatchewan" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Mexico Standard Time", TimeZoneValue = "(GMT-06:00) Guadalajara, Mexico City, Monterrey" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Central America Standard Time", TimeZoneValue = "(GMT-06:00) Central America" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Eastern Standard Time", TimeZoneValue = "(GMT-05:00) Eastern Time (US and Canada)" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "U.S. Eastern Standard Time", TimeZoneValue = "(GMT-05:00) Indiana (East)" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "S.A. Pacific Standard Time", TimeZoneValue = "(GMT-05:00) Bogota, Lima, Quito" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Atlantic Standard Time", TimeZoneValue = "(GMT-04:00) Atlantic Time (Canada)" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "S.A. Western Standard Time", TimeZoneValue = "(GMT-04:00) Caracas, La Paz" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Pacific S.A. Standard Time", TimeZoneValue = "(GMT-04:00) Santiago" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Newfoundland and Labrador Standard Time", TimeZoneValue = "(GMT-03:30) Newfoundland and Labrador" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "E. South America Standard Time", TimeZoneValue = "(GMT-03:00) Brasilia" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "S.A. Eastern Standard Time", TimeZoneValue = "(GMT-03:00) Buenos Aires, Georgetown" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Greenland Standard Time", TimeZoneValue = "(GMT-03:00) Greenland" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Mid-Atlantic Standard Time", TimeZoneValue = "(GMT-02:00) Mid-Atlantic" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Azores Standard Time", TimeZoneValue = "(GMT-01:00) Azores" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Cape Verde Standard Time", TimeZoneValue = "(GMT-01:00) Cape Verde Islands" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "GMT Standard Time", TimeZoneValue = "(GMT) Greenwich Mean Time: Dublin, Edinburgh, Lisbon, London" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Greenwich Standard Time", TimeZoneValue = "(GMT) Casablanca, Monrovia" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Central Europe Standard Time", TimeZoneValue = "(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Central European Standard Time", TimeZoneValue = "(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Romance Standard Time", TimeZoneValue = "(GMT+01:00) Brussels, Copenhagen, Madrid, Paris" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "W. Europe Standard Time", TimeZoneValue = "(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "W. Central Africa Standard Time", TimeZoneValue = "(GMT+01:00) West Central Africa" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "E. Europe Standard Time", TimeZoneValue = "(GMT+02:00) Bucharest" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Egypt Standard Time", TimeZoneValue = "(GMT+02:00) Cairo" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "FLE Standard Time", TimeZoneValue = "(GMT+02:00) Helsinki, Kiev, Riga, Sofia, Tallinn, Vilnius" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "GTB Standard Time", TimeZoneValue = "(GMT+02:00) Athens, Istanbul, Minsk" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Israel Standard Time", TimeZoneValue = "(GMT+02:00) Jerusalem" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "South Africa Standard Time", TimeZoneValue = "(GMT+02:00) Harare, Pretoria" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Russian Standard Time", TimeZoneValue = "(GMT+03:00) Moscow, St. Petersburg, Volgograd" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Arab Standard Time", TimeZoneValue = "(GMT+03:00) Kuwait, Riyadh" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "E. Africa Standard Time", TimeZoneValue = "(GMT+03:00) Nairobi" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Arabic Standard Time", TimeZoneValue = "(GMT+03:00) Baghdad" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Iran Standard Time", TimeZoneValue = "(GMT+03:30) Tehran" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Arabian Standard Time", TimeZoneValue = "(GMT+04:00) Abu Dhabi, Muscat" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Caucasus Standard Time", TimeZoneValue = "(GMT+04:00) Baku, Tbilisi, Yerevan" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Transitional Islamic State of Afghanistan Standard Time", TimeZoneValue = "(GMT+04:30) Kabul" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Ekaterinburg Standard Time", TimeZoneValue = "(GMT+05:00) Ekaterinburg" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "West Asia Standard Time", TimeZoneValue = "(GMT+05:00) Islamabad, Karachi, Tashkent" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "India Standard Time", TimeZoneValue = "(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Nepal Standard Time", TimeZoneValue = "(GMT+05:45) Kathmandu" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Central Asia Standard Time", TimeZoneValue = "(GMT+06:00) Astana, Dhaka" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Sri Lanka Standard Time", TimeZoneValue = "(GMT+06:00) Sri Jayawardenepura" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "N. Central Asia Standard Time", TimeZoneValue = "(GMT+06:00) Almaty, Novosibirsk" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Myanmar Standard Time", TimeZoneValue = "(GMT+06:30) Yangon Rangoon" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "S.E. Asia Standard Time", TimeZoneValue = "(GMT+07:00) Bangkok, Hanoi, Jakarta" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "North Asia Standard Time", TimeZoneValue = "(GMT+07:00) Krasnoyarsk" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "China Standard Time", TimeZoneValue = "(GMT+08:00) Beijing, Chongqing, Hong Kong SAR, Urumqi" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Singapore Standard Time", TimeZoneValue = "(GMT+08:00) Kuala Lumpur, Singapore" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Taipei Standard Time", TimeZoneValue = "(GMT+08:00) Taipei" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "W. Australia Standard Time", TimeZoneValue = "(GMT+08:00) Perth" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "North Asia East Standard Time", TimeZoneValue = "(GMT+08:00) Irkutsk, Ulaanbaatar" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Korea Standard Time", TimeZoneValue = "(GMT+09:00) Seoul" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Tokyo Standard Time", TimeZoneValue = "(GMT+09:00) Osaka, Sapporo, Tokyo" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Yakutsk Standard Time", TimeZoneValue = "(GMT+09:00) Yakutsk" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "A.U.S. Central Standard Time", TimeZoneValue = "(GMT+09:30) Darwin" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Cen. Australia Standard Time", TimeZoneValue = "(GMT+09:30) Adelaide" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "A.U.S. Eastern Standard Time", TimeZoneValue = "(GMT+10:00) Canberra, Melbourne, Sydney" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "E. Australia Standard Time", TimeZoneValue = "(GMT+10:00) Brisbane" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Tasmania Standard Time", TimeZoneValue = "(GMT+10:00) Hobart" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Vladivostok Standard Time", TimeZoneValue = "(GMT+10:00) Vladivostok" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "West Pacific Standard Time", TimeZoneValue = "(GMT+10:00) Guam, Port Moresby" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Central Pacific Standard Time", TimeZoneValue = "(GMT+11:00) Magadan, Solomon Islands, New Caledonia" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Fiji Islands Standard Time", TimeZoneValue = "(GMT+12:00) Fiji Islands, Kamchatka, Marshall Islands" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "New Zealand Standard Time", TimeZoneValue = "(GMT+12:00) Auckland, Wellington" });
                    context.LocationTimeZones.Add(new LocationTimeZone() { Name = "Tonga Standard Time", TimeZoneValue = "(GMT+13:00) Nuku'alofa" });

                    context.SaveChanges();
                }

                // SEED DATA FOR COUNTRY TABLE
                if (!context.Countries.Any())
                {
                    context.Countries.Add(new Country() { Code = "UAE", Description = "United Arab Emirates" });
                    context.Countries.Add(new Country() { Code = "Bangladesh", Description = "Bangladesh" });
                    context.Countries.Add(new Country() { Code = "Brunei", Description = "Brunei Darussalam" });
                    context.Countries.Add(new Country() { Code = "Indonesia", Description = "Indonesia" });
                    context.Countries.Add(new Country() { Code = "India", Description = "India" });
                    context.Countries.Add(new Country() { Code = "Japan", Description = "Japan" });
                    context.Countries.Add(new Country() { Code = "SriLanka", Description = "Sri Lanka" });
                    context.Countries.Add(new Country() { Code = "Myanmar", Description = "Myanmar" });
                    context.Countries.Add(new Country() { Code = "Maldives", Description = "Maldives" });
                    context.Countries.Add(new Country() { Code = "Malaysia", Description = "Malaysia" });
                    context.Countries.Add(new Country() { Code = "Oman", Description = "Oman" });
                    context.Countries.Add(new Country() { Code = "Qatar", Description = "Qatar" });
                    context.Countries.Add(new Country() { Code = "Singapore", Description = "Singapore" });
                    context.Countries.Add(new Country() { Code = "Thailand", Description = "Thailand" });
                    context.SaveChanges();
                }

                // SEED DATA FOR MEMBERROLE TABLE
                if (!context.MemberRoles.Any())
                {
                    context.MemberRoles.Add(new MemberRole() { MemberRoleName = "Admin" });
                    context.MemberRoles.Add(new MemberRole() { MemberRoleName = "Manager" });
                    context.MemberRoles.Add(new MemberRole() { MemberRoleName = "Member" });
                    context.SaveChanges();
                }


            }  
        }

        // CREATE FIRST USER CODE

        public static void CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<UserRoleIntPK>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = null;

            

           using (var context2 = serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                context2.Database.EnsureCreated();
                roleNames = context2.MemberRoles.ToList().Select(a => a.MemberRoleName).ToArray();
            
            

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {

                bool roleExist = false;

                Task.Run(async () =>
                {
                    roleExist = await RoleManager.RoleExistsAsync(role);
                }).GetAwaiter().GetResult();
               


                if (!roleExist)
                {
                  
                    Task.Run(async () =>
                    {
                        roleResult = await RoleManager.CreateAsync(new UserRoleIntPK(role));
                    }).GetAwaiter().GetResult();


                }
            }

            //    var powerUser = new ApplicationUser()
            //    {
            //        UserName = configuration.GetSection("AdminSettings")["AdminEmail"],
            //        Email = configuration.GetSection("AdminSettings")["AdminEmail"],
            //        Password = configuration.GetSection("AdminSettings")["AdminPassword"],
            //        ConfirmPassword = configuration.GetSection("AdminSettings")["AdminPassword"],
            //        CompanyId = 1,
            //        FirstName = "test1",
            //        LastName = "test name",
            //        AccessFailedCount = 1
                  
            //};

            //string UserPassword = configuration.GetSection("AdminSettings")["AdminPassword"];

            //ApplicationUser _user = null;

            //Task.Run(async () =>
            //{
            //    _user = await UserManager.FindByEmailAsync(configuration.GetSection("AdminSettings")["AdminEmail"]);
            //}).GetAwaiter().GetResult();


            //if (_user == null)
            //{
        
            //    Task.Run(async () =>
            //    {
            //        var CreatePowerUser = await UserManager.CreateAsync(powerUser, UserPassword);
            //        if (CreatePowerUser.Succeeded)
            //        {

            //            Task.Run(async () =>
            //            {
            //                await UserManager.AddToRoleAsync(powerUser, configuration.GetSection("AdminSettings")["AdminEmail"]);
            //            }).GetAwaiter().GetResult();

            //        }


            //    }).GetAwaiter().GetResult();

              
          

            //}

                // END HERE
            }
        }




    }


    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, UserRoleIntPK>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
            });

            services.AddTransient<IServiceProvider, ServiceProvider>();


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.AddMvc()
                .AddSessionStateTempDataProvider();

            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession();


        }


      
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env , IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();


            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Surveys}/{action=Create}/{id?}");
            });


            //COMMENT ON MODEL CREATING
            using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
            {
                context.Database.EnsureCreated();
                var aCompanies = context.Companies.Select(a => a.CompanyCode).ToArray();

                var rewrite = new RewriteOptions().Add(new CompaniesRedirectRule(
                     matchPaths: aCompanies,
                     newPath: "/Surveys/Create/"));

                app.UseRewriter(rewrite);

            }


            //SeedData.Initialize(app.ApplicationServices);
        }
    }
}
