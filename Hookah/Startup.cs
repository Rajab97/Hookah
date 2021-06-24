using Hookah.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hookah.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Hookah.Abstracts;
using Hookah.Services;
using Hookah.Interfacas;
using Hookah.Servicefacades;
using Hookah.Constants;
using Microsoft.AspNetCore.Authorization;
using Hookah.Authorization.Handlers;
using Hookah.Authorization.Requirements;

namespace Hookah
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
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(connectionString);
            });
            services.AddIdentity<User, IdentityRole<Guid>>(options => {
                //options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                //options.SignIn.RequireConfirmedPhoneNumber = true;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.LoginPath = "/Administration/Account/SignIn";
                options.AccessDeniedPath = "/Administration/Account/NotAuthorize";
                options.LogoutPath = "/Administration/Account/Logout";
                options.SlidingExpiration = false;
                //options.SlidingExpiration = true;
            });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
            });

            var profiles = this.GetType().Assembly.GetTypes().Where(m => m.IsClass && typeof(Profile).IsAssignableFrom(m));
            var mapperConfiguration = new AutoMapper.MapperConfiguration(mc => {
                foreach (var profile in profiles)
                {
                    mc.AddProfile(profile);
                }
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthentication();
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder().AddRequirements(new OnlyActiveUsersRequirement()).Build();
                options.AddPolicy(PolicyNames.SuperAdminRoleRequired, m => {
                    m.RequireRole(RoleNames.SuperAdmin);
                });
            });

            services.AddScoped<IAuthorizationHandler, OnlyActiveUsersHandler>();

            services.AddHttpContextAccessor();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            this.RegisterServices(services);
            this.RegisterServiceFacades(services);
            services.AddControllersWithViews();
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
                //app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Home/HandleError", "?statusCode={0}");

            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "area",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<>),typeof(BaseService<>));
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IMenuFruitHeadService, MenuFruitHeadService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenuFlavorService, MenuFlavorService>();
            services.AddScoped<IContactService, ContactService>();

        }
        private void RegisterServiceFacades(IServiceCollection services)
        {
            services.AddScoped<IPackageServiceFacade, PackageServiceFacade>();
            services.AddScoped<IMenuFruitHeadServiceFacade, MenuFruitHeadServiceFacade>();
            services.AddScoped<IMenuServiceFacade, MenuServiceFacade>();
            services.AddScoped<IMenuFlavorServiceFacade, MenuFlavorServiceFacade>();
            services.AddScoped<IContactServiceFacade, ContactServiceFacade>();


        }
    }
}
