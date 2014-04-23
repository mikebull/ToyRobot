using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoneProject.Logic
{
    public enum ToyAction
    {
        Move,
        Left,
        Right
    }

    public enum ToyDirection
    {
        North,
        South,
        East,
        West
    }
    
    public enum Commands
    {
        Place,
        Move,
        Left,
        Right,
        Report
    }

    public enum Status
    {
        Toy,
        Open
    }
}
