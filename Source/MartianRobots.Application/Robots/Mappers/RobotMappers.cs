using MartianRobots.Application.Robots.DTOs;
using MartianRobots.Domain.MapsAggregate;
using MartianRobots.Domain.RobotsAggregate;

namespace MartianRobots.Application.Robots.Mappers;

public static class RobotMappers
{
    public static RobotDto AsDto(this Robot robot)
    {
        if (robot == null)
        {
            throw new ArgumentNullException(nameof(robot));
        }

        return new RobotDto(robot.Id, robot.Name);
    }

    public static RobotReportDto AsReport(this Robot robot)
    {
        if (robot == null)
        {
            throw new ArgumentNullException(nameof(robot));
        }

        return new RobotReportDto(robot.Name, robot.Coordinates!.XPos, robot.Coordinates.YPos, robot.Orientation, robot.RobotMode);
    }
}