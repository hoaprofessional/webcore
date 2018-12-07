using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Globalization;
using WebCore.Entities;
using WebCore.EntityFramework.Data;
using WebCore.EntityFramework.Helper;
using WebCore.EntityFramework.Repositories;
using WebCore.EntityFramework.Seeds;
using WebCore.Helper;
using WebCore.Services.Impl.Admins.LanguageDetails;
using WebCore.Services.Impl.Admins.Languages;
using WebCore.Services.Impl.Admins.Users;
using WebCore.Services.Impl.AppMenus;
using WebCore.Services.Impl.Languages;
using WebCore.Services.Impl.SystemConfigs;
using WebCore.Services.Share.Admins.LanguageDetails;
using WebCore.Services.Share.Admins.Languages;
using WebCore.Services.Share.Admins.Users;
using WebCore.Services.Share.AppMenus;
using WebCore.Services.Share.Helper;
using WebCore.Services.Share.Languages;
using WebCore.Services.Share.SystemConfigs;

namespace WebCore
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<WebCoreDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<WebCoreUser, WebCoreRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<WebCoreDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<WebCoreUserStore>();
            services.AddTransient<WebCoreRoleStore>();

            services.AddTransient<IEmailSender, WebCoreMailSender>();


            services.AddScoped(typeof(IRepository<,>), typeof(RepositoryImpl<,>));


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ILanguageProviderService, LanguageProviderService>();
            services.AddScoped<ILanguageAdminService, LanguageAdminService>();
            services.AddScoped<ILanguageDetailAdminService, LanguageDetailAdminService>();
            services.AddScoped<Services.Share.Admins.AdminMenus.IAdminMenuAdminService, Services.Impl.Admins.AdminMenus.AdminMenuAdminService>();
            services.AddScoped<Services.Share.Admins.MasterLists.IMasterListAdminService, Services.Impl.Admins.MasterLists.MasterListAdminService>();
            services.AddScoped<Services.Share.Admins.MasterListGroups.IMasterListGroupAdminService, Services.Impl.Admins.MasterListGroups.MasterListGroupAdminService>();
            services.AddScoped<Services.Share.RecordStatuss.IRecordStatusHelper, Services.Impl.RecordStatuss.RecordStatusHelper>();
            services.AddScoped<Services.Share.AdminMenus.IAdminMenuProvider, Services.Impl.AdminMenus.AdminMenuProvider>();
            services.AddScoped<ISystemConfigService, SystemConfigService>();
            services.AddScoped<IAppMenuService, AppMenuService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<Services.Share.Admins.Roles.IRoleService, Services.Impl.Admins.Roles.RoleService>();
            services.AddScoped<Services.Share.Permissions.IPermissionService, Services.Impl.Permissions.PermissionService>();
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.TryAddSingleton<IUrlHelperFactory, UrlHelperFactory>();


            MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
            mc.AddProfile(new MappingProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            //services.Scan(scan =>
            //    scan.FromAssemblyOf<ILanguageService>()
            //        .AddClasses(classes => classes.Where(i => i.Name.EndsWith("Service")))
            //        .AsImplementedInterfaces()
            //        .WithScopedLifetime());

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(60000);
                options.Cookie.HttpOnly = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default1",
                    template: "index.html",
                    new
                    {
                        controller = "Home",
                        action = "Index"
                    });
            });


            //var supportedCultures = new[]
            //{
            //    new CultureInfo("vi-VN"),
            //    new CultureInfo("en-US"),
            //};

            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("vi-VN"),
            //    // Formatting numbers, dates, etc.
            //    SupportedCultures = supportedCultures,
            //    // UI strings that we have localized.
            //    SupportedUICultures = supportedCultures
            //});

            CultureInfo.CurrentCulture = new CultureInfo("vi-VN");


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
