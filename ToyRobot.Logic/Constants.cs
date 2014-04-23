using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoneProject.Logic
{
    public class Constants
    {
        public const string Place = "PLACE";
        public const string Move = "MOVE";
        public const string Left = "LEFT";
        public const string Right = "RIGHT";
        public const string Report = "REPORT";

        public const string North = "North";
        public const string South = "South";
        public const string East = "East";
        public const string West = "West";
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
