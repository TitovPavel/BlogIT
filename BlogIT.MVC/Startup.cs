using AutoMapper;
using BlogIT.DB.BL;
using BlogIT.DB.DAL;
using BlogIT.DB.Models;
using BlogIT.MVC.SignalR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace BlogIT.MVC
{
    public class Startup
    {

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<BlogITContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BlogITDatabase")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BlogITContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddSingleton(Configuration);

            services.AddAutoMapper(typeof(Mappings.MappingProfile));

            services.AddAuthorization();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                    options.LoginPath = new PathString("/Account/Login");
                })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddSignalR(options => options.EnableDetailedErrors = true);

            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.SubFolder,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddFluentValidation(fv =>
                    {
                        fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                        fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("ru"),
                    new CultureInfo("en")
                };

                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRequestLocalization();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
