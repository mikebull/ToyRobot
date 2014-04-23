using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoneProject.Logic
{
    /// <summary>
    /// Commonly used string constants used 
    /// in application
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Constants for game commands
        /// </summary>
        public class Commands
        {
            public const string Place = "PLACE";
            public const string Move = "MOVE";
            public const string Left = "LEFT";
            public const string Right = "RIGHT";
            public const string Report = "REPORT";
        }

        /// <summary>
        /// Constants to represent cardinal points
        /// </summary>
        public class CardinalPoints
        {
            public const string North = "North";
            public const string South = "South";
            public const string East = "East";
            public const string West = "West";
        }
    }
}
