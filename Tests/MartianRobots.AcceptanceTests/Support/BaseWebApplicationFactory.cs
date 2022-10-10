using MartianRobots.API;
using MartianRobots.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MartianRobots.AcceptanceTests.Support
{
    public class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
    {
        protected BaseWebApplicationFactory()
        {
            this.HttpClient = this.CreateClient();
        }

        public HttpClient HttpClient { get; }

        protected IServiceProvider ServiceProvider { get; private set; }

        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()!
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>();
                })
                .ConfigureHostConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.Testing.json", false);
                    config.AddEnvironmentVariables("ASPNETCORE");
                })
                .UseEnvironment("Testing");
        }            

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices((configuration, services) =>
            {
                services.AddDbContextFactory<MartianRobotsContext>(options =>
                {
                    options.UseSqlServer(configuration.Configuration.GetConnectionString("DefaultConnection"));
                });

                this.ServiceProvider = services.BuildServiceProvider();

                using var scope = this.ServiceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<MartianRobotsContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                this.SeedDatabase(context);
            });
        }

        protected virtual void SeedDatabase(MartianRobotsContext context)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                switch (this.ServiceProvider)
                {
                    case IDisposable disposable:
                        disposable.Dispose();
                        break;
                }
            }

            base.Dispose(disposing);
        }
    }
}
