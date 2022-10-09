using System.Text.Json.Serialization;
using MartianRobots.API.Mappers;
using MartianRobots.API.Models;
using MartianRobots.API.Seed;
using MartianRobots.Application;
using MartianRobots.Application.Maps.DTOs;
using MartianRobots.Application.Maps.Queries;
using MartianRobots.Application.Robots.Commands;
using MartianRobots.Application.Robots.DTOs;
using MartianRobots.Application.Robots.Queries;
using MartianRobots.Domain;
using MartianRobots.Infrastructure;
using MartianRobots.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.PropertyNamingPolicy = null;        
    })
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddDomain()
    .AddApplication()
    .AddEntityFramework(connectionString)
    .AddInfrastructure();   

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/maps/{name}", async (IMediator mediator, string name) =>

    await mediator.Send(new GetMapQuery(name))
    is MapDto map
            ? Results.Ok(map)
            : Results.NotFound()
);

app.MapGet("/robots/{name}", async (IMediator mediator, string name) =>

    await mediator.Send(new GetRobotQuery(name))
    is RobotDto robot
            ? Results.Ok(robot)
            : Results.NotFound()
);

app.MapGet("/robots/{id}/report", async (IMediator mediator, Guid id) =>

    await mediator.Send(new GetRobotReportQuery(id))
    is RobotReportDto robotReport
            ? Results.Ok(robotReport)
            : Results.NotFound()
);

app.MapPost("/robots/{id}/place", async (IMediator mediator, Guid id, PlaceRobotModel model) =>
{
    var result = await mediator.Send(new PlaceOnCommand(id, model.AsDto()));

    return result.Success ? Results.Ok() : Results.BadRequest(result.Error);
});

app.MapPost("/robots/{id}/move", async (IMediator mediator, Guid id, MoveRobotModel model) =>
{
    var result = await mediator.Send(new MoveCommand(id, model.AsDto()));

    return result.Success ? Results.Ok() : Results.BadRequest(result.Error);
});

app.MigrateDatabase<MartianRobotsContext>()
    .LoadMap()
    .LoadRobot();

app.Run();