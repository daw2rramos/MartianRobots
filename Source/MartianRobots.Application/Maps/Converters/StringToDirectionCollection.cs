using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;

namespace MartianRobots.Application.Maps.Converters
{
    public class StringToDirectionCollection
    {
        public static List<Direction> Convert(string instructionsAsString)
        {
            Guards.ThrowIfNullOrEmpty(instructionsAsString);

            var instructions = new List<Direction>();

            foreach (var instruction in instructionsAsString.ToUpper())
            {
                switch (instruction)
                {
                    case 'L':
                        instructions.Add(Direction.Left);
                        break;

                    case 'R':
                        instructions.Add(Direction.Right);
                        break;

                    case 'F':
                        instructions.Add(Direction.Forward);
                        break;
                }
            }

            return instructions;
        }
    }
}
