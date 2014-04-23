using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZoneProject;
using ZoneProject.Logic;
using ZoneProject.Logic.Models;

namespace ZoneProject
{
    public static class ParseCommands
    {
        /// <summary>
        /// Parse Place command
        /// </summary>
        /// <param name="input"></param>
        public static void Place(string input)
        {
            // Strip whitespace and remove command
            input = input.Replace(Constants.Place, String.Empty, StringComparison.InvariantCultureIgnoreCase);
            input = Regex.Replace(input, @"\s+", "");

            string[] values = input
                .Split(',')
                .Select(sValue => sValue.Trim())
                .ToArray();

            if (values.Count() != 3)
            {
                throw new ArgumentException("An incorrect place command has been given");
            }

            int x;
            int y;

            if (!Int32.TryParse(values[0], out x))
            {
                throw new InvalidCastException("Invalid coordinate given");
            } 

            if(!Int32.TryParse(values[1], out y))
            {
                throw new InvalidCastException("Invalid coordinate given");
            }

            ToyDirection direction = values[2].ParseEnum();

            Table.PlaceToy(x, y, direction);
        }

        public static void Move(string input)
        {
            Table.MoveToy(ToyAction.Move);
        }

        public static void Direction(string input)
        {
            if (input.Contains(Constants.Left, StringComparison.InvariantCultureIgnoreCase))
            {
                Table.ChangeToysDirection(ToyAction.Left);
            }
            else if (input.Contains(Constants.Right, StringComparison.InvariantCultureIgnoreCase))
            {
                Table.ChangeToysDirection(ToyAction.Right);
            }
        }
    }
}
