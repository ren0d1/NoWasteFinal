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
using NoWaste.Models;
using NoWaste.Services;
using NoWaste.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using NoWaste.Hubs;

namespace NoWaste
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 5;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireDigit = true;

                    //User settings
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration.GetSection("Authentication").GetSection("Facebook").GetValue<string>("AppId");
                facebookOptions.AppSecret = Configuration.GetSection("Authentication").GetSection("Facebook").GetValue<string>("AppSecret");
            });

            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = Configuration.GetSection("Authentication").GetSection("Twitter").GetValue<string>("ConsumerKey");
                twitterOptions.ConsumerSecret = Configuration.GetSection("Authentication").GetSection("Twitter").GetValue<string>("ConsumerSecret");
            });

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration.GetSection("Authentication").GetSection("Google").GetValue<string>("ClientId");
                googleOptions.ClientSecret = Configuration.GetSection("Authentication").GetSection("Google").GetValue<string>("ClientSecret");
            });

            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Configuration.GetSection("Authentication").GetSection("Microsoft").GetValue<string>("ApplicationId");
                microsoftOptions.ClientSecret = Configuration.GetSection("Authentication").GetSection("Microsoft").GetValue<string>("Password");
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<UnitOfWork, UnitOfWork>();

            //Enforce SSL
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            services.AddSignalR();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            RewriteOptions options = new RewriteOptions().AddRedirectToHttps();
            app.UseRewriter(options);

            app.UseStaticFiles();

            app.UseSignalR(routes =>
            {
                routes.MapHub<MessengerHub>("/messages");
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
