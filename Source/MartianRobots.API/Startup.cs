using MartianRobots.API.Mappers;
using MartianRobots.API.Models;
using MartianRobots.Application;
using MartianRobots.Application.Maps.DTOs;
using MartianRobots.Application.Maps.Queries;
using MartianRobots.Application.Robots.Commands;
using MartianRobots.Application.Robots.DTOs;
using MartianRobots.Application.Robots.Queries;
using MartianRobots.Domain;
using MartianRobots.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace MartianRobots.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
            .Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = null;
            })
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(setup => { 
                setup.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "MartianRobots.API", Version = "v1" }); 
            })
            .AddDomain()
            .AddApplication()
            .AddEntityFramework(this.Configuration.GetConnectionString("DefaultConnection"))
            .AddInfrastructure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(e =>
            {
                e.MapGet("/maps/{name}", async (IMediator mediator, string name) =>

                    await mediator.Send(new GetMapQuery(name))
                    is MapDto map
                            ? Results.Ok(map)
                            : Results.NotFound()
                );

                e.MapGet("/robots/{name}", async (IMediator mediator, string name) =>

                    await mediator.Send(new GetRobotQuery(name))
                    is RobotDto robot
                            ? Results.Ok(robot)
                            : Results.NotFound()
                );

                e.MapGet("/robots/{id}/report", async (IMediator mediator, Guid id) =>

                    await mediator.Send(new GetRobotReportQuery(id))
                    is RobotReportDto robotReport
                            ? Results.Ok(robotReport)
                            : Results.NotFound()
                );

                e.MapPost("/robots/{id}/place", async (IMediator mediator, Guid id, PlaceRobotModel model) =>
                {
                    var result = await mediator.Send(new PlaceOnCommand(id, model.AsDto()));

                    return result.Success ? Results.Ok() : Results.BadRequest(result.Error);
                });

                e.MapPost("/robots/{id}/move", async (IMediator mediator, Guid id, MoveRobotModel model) =>
                {
                    var result = await mediator.Send(new MoveCommand(id, model.AsDto()));

                    return result.Success ? Results.Ok() : Results.BadRequest(result.Error);
                });
            });
            

            //app.MigrateDatabase<MartianRobotsContext>()
            //    .LoadMap()
            //    .LoadRobot();

            //app.Run();
        }
    }
}
