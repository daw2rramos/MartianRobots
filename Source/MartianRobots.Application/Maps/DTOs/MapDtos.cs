using MartianRobots.Domain.MapsAggregate;

namespace MartianRobots.Application.Maps.DTOs
{
    public sealed record MapDto(Guid Id, string Name, int MarsWidth, int MarsHeight);
    public sealed record CoordinatesDto(int XPos, int YPos);
    public sealed record DeployInstructionDto(CoordinatesDto Coordinates, Orientation Orientation);
}
