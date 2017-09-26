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
using Event.Data;
using Event.Models;
using Event.Services;

namespace Event
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
            // Database
            var sqlConnectionString = "server=164.132.233.40;userid=switchlook;password=teoy3RroLKqqWpm0;database=switchlook;";
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    sqlConnectionString
                )
            );
           

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "496342054036347";
                facebookOptions.AppSecret = "0d4236e657b7710132e40d087f5ff10b";
            }).AddGoogle(googleOptions =>           
            {
                googleOptions.ClientId = "697330306989-6ur1rkdq2brslp5nvglurri3qj1025ut.apps.googleusercontent.com";
                googleOptions.ClientSecret = "3CE8GeiJDxXt-oJZDwfy-O6W";
            });
            

            services.AddMvc();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
