using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots.Domain.RobotsAggregate
{
    public enum RobotMode
    {
        Unknown,        
        InOperation,
        Waiting,
        Lost
    }
}
