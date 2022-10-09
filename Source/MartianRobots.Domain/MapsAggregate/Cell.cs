#pragma warning disable 8618
using MartianRobots.SharedKernel;

namespace MartianRobots.Domain.MapsAggregate;

public sealed class Cell : BaseEntity
{
    private Cell()
    {
    }

    public Cell(Coordinates coordinates, bool blocked)
    {
        Coordinates = Guards.ThrowIfNull(coordinates);
        Blocked = blocked;
    }

    public Coordinates Coordinates { get; private set; }

    public Guid? RobotId { get; private set; }

    public bool Blocked { get; private set; }

    private bool RobotExists => RobotId is not null && RobotId != Guid.Empty;

    public void Block()
    {
        Blocked = true;
    }

    public Result<Coordinates> CanBlock()
    {
        var message = $"Cell with coordinates x:{Coordinates.XPos} and y: {Coordinates.YPos} already blocked";
        if (RobotExists)
        {
            message += $" by robot with id: {RobotId}";
        }

        return Blocked ? Result.Fail<Coordinates>(message) : Result.Ok(Coordinates);
    }        
}