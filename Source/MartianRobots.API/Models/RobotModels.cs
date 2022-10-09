using MartianRobots.Domain.MapsAggregate;
using MartianRobots.Domain.RobotsAggregate;

namespace MartianRobots.API.Models
{
    public sealed record RobotModel(Guid Id, int XPos, int YPos);
    public sealed record RobotReportModel(string Name, int XPos, int YPos, Orientation Orientation, RobotMode RobotMode);
    public sealed record PlaceRobotModel(Guid MapId, string DeployInstruction);
    public sealed record MoveRobotModel(Guid MapId, string Instruction);
}
