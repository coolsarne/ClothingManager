using System;
using System.Net.Http;
using ClothingManager.BL;
using ClothingManager.DAL;
using ClothingManager.DAL.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UI.MVC.GraphQL;

namespace ClothingManager.UI.MVC
{
    public class Startup
    {
        private readonly string AllowedOrigin = "allowedOrigin";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ClothingManagerDbContext>(ServiceLifetime.Scoped);
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IManager, Manager>();
            services.AddControllersWithViews();
            services.AddGraphqlClient();

            services.AddHttpClient("rest", x => x.BaseAddress = new Uri("https://localhost:5002/"));
            services.AddHttpClient("GraphqlClient")
                .ConfigureHttpClient(x => x.BaseAddress = new Uri("https://localhost:5002/graphql"));
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
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                app.UseCors(AllowedOrigin);
                app.UseWebSockets();
                app.UseRouting();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Designer}/{action=Index}/{id?}");
            });

            //app.UseSwagger();
            //app.UseSwaggerUI();
        }
    }
}