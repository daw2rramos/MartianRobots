using MartianRobots.SharedKernel;

namespace MartianRobots.Domain.MapsAggregate.Services
{
    public class MapService : IMapService
    {       
        public Result<(int xStep, int yStep)> GetNextMovement(Coordinates coordinates, Orientation orientation)
        {            
            const int chunkAdvances = 1;

            var xStep = orientation == Orientation.E || orientation == Orientation.W ? chunkAdvances : 0;
            var yStep = orientation == Orientation.N || orientation == Orientation.S ? chunkAdvances : 0;            
            
            if (orientation == Orientation.S)
            {
                yStep *= -1;
            }

            if (orientation == Orientation.W)
            {
                xStep *= -1;
            }

            return Result.Ok((xStep, yStep));
        }           
    }
}
