using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;

namespace MartianRobots.Domain.RobotsAggregate
{
    public sealed class Robot : BaseEntity, IAggregateRoot
    {       
        public Robot(string name)
        {
            this.Name = Guards.ThrowIfNullOrEmpty(name);          
            this.RobotMode = RobotMode.Waiting;
        }               

        public string Name { get; }      

        public RobotMode RobotMode { get; private set; }

        public Coordinates? Coordinates { get; private set; }

        public Orientation Orientation { get; private set; }

        public void PlaceOn(int xPos, int yPos, Orientation orientation)
        {            
            this.Coordinates = Coordinates.Create(xPos, yPos);
            this.RobotMode = RobotMode.InOperation;
            this.Orientation = orientation;            
        }

        public Result<Coordinates> CanMove(Map map, int xStep, int yStep)
        {
            Guards.ThrowIfNull(map);

            if (this.Coordinates is null)
            {
                return Result.Fail<Coordinates>("Cannot move a robot with null initial coordinates");
            }

            if (this.RobotMode != RobotMode.InOperation)
            {
                return Result.Fail<Coordinates>($"Cannot move Robot in mode: {this.RobotMode}");
            }

            var coordinates = Coordinates.Create(this.Coordinates.XPos + xStep, this.Coordinates.YPos + yStep);

            var checkIsOutOfBounds = map.CheckAvailableCell(coordinates);

            if (checkIsOutOfBounds.Failure)
            {
                this.RobotMode = RobotMode.Lost;
            }

            return checkIsOutOfBounds;
        }

        public void MoveTo(Coordinates coordinates)
        {            
            Guards.ThrowIfNull(coordinates);

            if(this.Coordinates == coordinates)
            {
                return;
            }            

            this.Coordinates = coordinates;
        }

        public void TurnLeft(Orientation orientation)
        {
            this.Orientation = Turn(orientation, -1);
        }

        public void TurnRight(Orientation orientation)
        {
            this.Orientation = Turn(orientation, 1);
        }

        private static Orientation Turn(Orientation orientation, int times)
        {            
            var min = (int)Enum.GetValues(typeof(Orientation)).Cast<Orientation>().Min();
            var max = (int)Enum.GetValues(typeof(Orientation)).Cast<Orientation>().Max();

            var next = (int)orientation + times;
            if (next > max) next = min;
            if (next < min) next = max;

            return (Orientation)next;
        }
    }
}
