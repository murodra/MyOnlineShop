using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyOnlineShop.Data;
using MyOnlineShop.Data.Entities;
using MyOnlineShop.Services.Concrete;
using MyOnlineShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineShop.Ui
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyOnlineShopDbContext>(option =>
            {
                option.UseSqlServer(connectionString: "Server=.;Database=MyOnlineShopDb;Trusted_Connection=True");
            });
            services.AddScoped<IRepository<Product>, EFRepository<Product>>();
            services.AddScoped<IRepository<Order>, EFRepository<Order>>();
            services.AddScoped<IRepository<Category>, EFRepository<Category>>();
            services.AddScoped<IRepository<Store>, EFRepository<Store>>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = "/auth/login";
                option.AccessDeniedPath = "/auth/accessdenied";
                option.ExpireTimeSpan = TimeSpan.FromHours(1);
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>
            //   (ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationSignInManager>
            //   (ApplicationSignInManager.Create);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}",
                defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
