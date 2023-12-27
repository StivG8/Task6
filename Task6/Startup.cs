using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Task6.Data;
using Task6.Hubs;
using Task6.Repositories;

namespace Task6
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add db context
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSignalR(o => { o.EnableDetailedErrors = true; });

            services.AddScoped<ElementRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await Task.Run(() => context.Response.Redirect("/index.html")); });
                endpoints.MapHub<CanvasHub>("/canvas");
            });
        }
    }
}
