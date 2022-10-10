using MartianRobots.API;
using MartianRobots.API.Seed;
using MartianRobots.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
CreateHostBuilder(args)
    .Build()
    .MigrateDatabase<MartianRobotsContext>() // Don't do this in production NEVER!!!!
    .LoadMap()
    .LoadRobot()
    .Run();

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}