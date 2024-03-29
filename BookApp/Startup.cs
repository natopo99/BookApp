using BookApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp
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
            services.AddScoped<IBookAppRepository, EFBookAppRepository>();
            services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();
            services.AddRazorPages(); // this allows us to work with razor pages

            services.AddDistributedMemoryCache(); // this creates a session so that the cart doesnt get reset
            services.AddSession();
            services.AddDbContext<BookstoreContext>(options => options.UseSqlite(Configuration["ConnectionStrings:BookDBConnection"]));

            services.AddScoped<Basket>(x => SessionBasket.GetBasket(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();





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
            app.UseSession(); // this creates a session so the items in the cart dont get lost when changing the page 
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "categoryPage",
                    pattern: "{category}/Page{pageNum}", // Change how the URL is shown on the search pn bar when you go to different pages
                    defaults: new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page{pageNum}", // Change how the URL is shown on the search pn bar when you go to different pages
                    defaults: new { controller = "Home", action = "Index" , pageNum = 1});

                endpoints.MapControllerRoute(
                    name: "category",
                    pattern: "{category}", // Change how the URL is shown on the search pn bar when you go to different pages
                    defaults: new { controller = "Home", action = "Index", pageNum = 1});

                endpoints.MapDefaultControllerRoute(); 
                endpoints.MapRazorPages();
                
            });
        }
    }
}
