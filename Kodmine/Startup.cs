using Kodmine.Core.Interfaces;
using Kodmine.DAL;
using Kodmine.DAL.Models;
using Kodmine.DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Kodmine
{
    public class Startup
    {
        public static IServiceProvider ServiceMan { get; private set; }
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>()
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                            optional: false,
                            reloadOnChange: true);
            }
            else
            {
                builder
                    .AddJsonFile("appsettings.Development.json")
                    .AddJsonFile($"appsettings.json",
                        optional: false,
                        reloadOnChange: true);
            }

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //var connection = @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0";

            //services.AddDbContext<KodmineDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
#if DEBUG
            services.AddDbContext<KodmineDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
#else
            services.AddDbContext<KodmineDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HostingConnection")));
#endif

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<KodmineDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                //options.ExpireTimeSpan = TimeSpan.FromDays(10);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            //.AddRazorPagesOptions(options =>
            //{
            //    options.Conventions.AuthorizeFolder("/Account/Manage");
            //    options.Conventions.AuthorizePage("/Account/Logout");
            //});

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            //    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
            //    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            //});

            services.AddAuthorization(options =>
            {
                options.AddPolicy("PostPolicy", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Administrator"));
            });

            //services.AddSession();

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            //services.AddSingleton<IEmailSender, EmailSender>();

            services.AddSingleton<IConfiguration>(Configuration);
            //Everything related to the db context should live only within one request, 
            //so it should be scoped
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostTagRepository, PostTagRepository>();
            services.AddScoped<IRubricRepository, RubricRepository>();

            services.AddMvc();

            ServiceMan = services.BuildServiceProvider();

            //var r = ServiceMan.GetService<IRubricRepository>();
            //services.AddSingleton<DynamicLayoutService>(s => {
            //    return new DynamicLayoutService(r);
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //app.Use(async (context, next) =>
            //{
            //    context.Items.Add("rub", "rubric");
            //    await next.Invoke();
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseExceptionHandler("/Error");

            app.UseStaticFiles();

            app.UseAuthentication();

            //app.UseSession();

            //app.UseDynamicLayoutMiddleware();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
