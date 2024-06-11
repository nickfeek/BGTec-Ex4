using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AzureFileStorageApi.Data;

namespace AzureFileStorageApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add controllers to the services container
            services.AddControllers();

            // Add DbContext to the services container, using DataContext class
            services.AddDbContext<DataContext>();

            // Add scoped dependency injection for the data repository interface and its implementation
            services.AddScoped<IDataRepository, DataRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // If the environment is Development, use the developer exception page
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add routing middleware to the request pipeline
            app.UseRouting();

            // Configure the app to use endpoints, mapping controller endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Initialize the database by ensuring it is created
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<DataContext>();
                db?.Database.EnsureCreated();
            }
        }
    }
}
