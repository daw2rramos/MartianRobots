using MartianRobots.Domain.MapsAggregate;
using MartianRobots.Domain.RobotsAggregate;

namespace MartianRobots.Application.Robots.DTOs;

public sealed record RobotDto(Guid Id, string Name);
public sealed record RobotReportDto(string Name, int XPos, int YPos, Orientation Orientation, RobotMode RobotMode);
public sealed record PlaceRobotDto(Guid MapId, string DeployInstruction);
public sealed record MoveRobotDto(Guid MapId, string Instructions);