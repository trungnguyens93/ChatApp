using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Identity.CustomIdentityDB.Factories;
using Identity.CustomIdentityDB.Models;
using Identity.CustomIdentityDB.Providers;
using Identity.CustomIdentityDB.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity.CustomIdentityDB
{
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
            services.AddControllersWithViews();

            var connectionString = @"Server=.;Database=CustomIdentityUserDB;User Id=sa;Password=Admin@123;";
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<CustomIdentityDbContext>(options => options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly)));

            services
                .AddIdentity<CustomIdentityUser, IdentityRole>(options =>
                {
                    // options.SignIn.RequireConfirmedEmail = true;
                    options.Tokens.EmailConfirmationTokenProvider = "emailconf";

                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 4;

                    options.User.RequireUniqueEmail = true;

                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                })
                .AddEntityFrameworkStores<CustomIdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<CustomIdentityUser>>("emailconf")
                .AddPasswordValidator<DoesNotContainPasswordValidator<CustomIdentityUser>>();

            services.AddScoped<IUserStore<CustomIdentityUser>, UserOnlyStore<CustomIdentityUser, CustomIdentityDbContext>>();
            services.AddScoped<IUserClaimsPrincipalFactory<CustomIdentityUser>, CustomUserClaimsPrincipalFactory>();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(3);
            });

            services.Configure<EmailConfirmationTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(2);
            });

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");

            services.AddAuthentication()
                .AddGoogle("google", options =>
                {
                    options.ClientId = "791085069039-cas6urvvl7fqq5ri3dqbr28lih7jb1qj.apps.googleusercontent.com";
                    options.ClientSecret = "iXK4guuofMgMkfz_889y3Av6";
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
