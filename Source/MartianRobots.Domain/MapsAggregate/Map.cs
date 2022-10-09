#pragma warning disable 8618
using MartianRobots.SharedKernel;

namespace MartianRobots.Domain.MapsAggregate;

public sealed class Map : BaseEntity, IAggregateRoot
{
    private readonly List<Cell> cells;

    private Map() { }

    public Map(string name, IEnumerable<Cell> cells)
    {
        Name = name;
        this.cells = Guards.ThrowIfNull(cells).ToList();
    }

    public string Name { get; private set; }

    public int Height => cells.Max(x => x.Coordinates.YPos);
    public int Width => cells.Max(x => x.Coordinates.XPos);

    public IReadOnlyList<Cell> Cells => cells.ToList();    

    public Result<Coordinates> CheckAvailableCell(Coordinates coordinates)
    {
        Guards.ThrowIfNull(coordinates);

        var cell = cells.FirstOrDefault(x => x.Coordinates == coordinates);        

        return cell is null ?
            Result.Fail<Coordinates>($"Coordinates x: {coordinates.XPos}, y: {coordinates.YPos} not found in this map") :
            cell.CanBlock();
    }    
}