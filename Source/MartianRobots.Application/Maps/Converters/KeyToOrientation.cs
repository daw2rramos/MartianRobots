using MartianRobots.Domain.MapsAggregate;
using MartianRobots.SharedKernel;

namespace MartianRobots.Application.Maps.Converters
{
    public class KeyToOrientation
    {
        public static Orientation Convert(string key)
        {
            Guards.ThrowIfNullOrEmpty(key);

            return key.ToUpper() switch
               {
                   "N" => Orientation.N,
                   "S" => Orientation.S,
                   "E" => Orientation.E,
                   "W" => Orientation.W,
                   _ => throw new ArgumentException(message: "Invalid orientation code value", paramName: nameof(key)),
               };
        }
    }
}
