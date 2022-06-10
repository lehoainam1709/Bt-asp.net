using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using Microsoft.AspNetCore.Http;

namespace PetShop
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

            services.AddDbContext<PetShopContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PetShopContext")));
            services.AddScoped<IPetShopRepository,EFPetShopRepository>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();
            services.AddRazorPages();           
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddScoped<MyCart>(sp => MySessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddServerSideBlazor();
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
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("spepage", "{species}/{ProductPage:int}",
                new { Controller = "Home", action = "ThuCung" });
                endpoints.MapControllerRoute("default", "{controller}/{action}",
                new { controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("page", "{ProductPage:int}",
                new { Controller = "Home", action = "ThuCung", ProductPage = 1 });
                endpoints.MapControllerRoute("species", "{species}",
                new { Controller = "Home", action = "ThuCung", ProductPage = 1 });
                endpoints.MapControllerRoute("pagination", "Pet/{ProductPage}",
                new { Controller = "Home", action = "ThuCung", ProductPage = 1 });
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/admin/{*catchall}",
               "/Admin/Index");
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
