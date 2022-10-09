using MartianRobots.Domain.MapsAggregate;

namespace MartianRobots.API.Seed
{
    public static class MapSeed
    {
        public static IEnumerable<Cell> GetDefaultMapCellConfiguration()
        {
            var cellCollection = new List<Cell>(24);
            for (var yPos = 0; yPos < 4; yPos++)
            {
                for (var xPos = 0; xPos < 6; xPos++)
                {
                    cellCollection.Add(new Cell(Coordinates.Create(xPos, yPos), false));
                }
            }          

            return cellCollection;
        }
    }
}
