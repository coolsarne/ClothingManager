using ClothingManager.DAL;
using ClothingManager.DAL.EF;
using GraphQlService.GraphQL;
using HotChocolate.AspNetCore.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace GraphQlService
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
            services.AddHttpResultSerializer(
                batchSerialization: HttpResultSerialization.JsonArray
            );

            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>();
            services.AddControllers();
            
                //.AddNewtonsoftJson();
            var context = new ClothingManagerDbContext();
            services.AddSingleton<IRepository>(new Repository(context));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapControllers();
            });
        }
    }
}