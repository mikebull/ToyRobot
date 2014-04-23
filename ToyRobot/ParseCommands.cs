using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToyRobot;
using ToyRobot.Logic;
using ToyRobot.Logic.Models;

namespace ToyRobot
{
    /// <summary>
    /// Abstracted commands for game
    /// </summary>
    public static class ParseCommands
    {
        /// <summary>
        /// Parse Place command
        /// </summary>
        /// <param name="input"></param>
        public static void Place(string input)
        {
            // Strip whitespace and remove command
            input = input.Replace(Constants.Commands.Place, String.Empty, StringComparison.InvariantCultureIgnoreCase);
            input = Regex.Replace(input, @"\s+", String.Empty);

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

        /// <summary>
        /// Parse move command
        /// </summary>
        /// <param name="input"></param>
        public static void Move(string input)
        {
            Table.MoveToy(ToyAction.Move);
        }

        /// <summary>
        /// Parse direction command
        /// </summary>
        /// <param name="input"></param>
        public static void Direction(string input)
        {
            if (input.Contains(Constants.Commands.Left, StringComparison.InvariantCultureIgnoreCase))
            {
                Table.ChangeToysDirection(ToyAction.Left);
            }
            else if (input.Contains(Constants.Commands.Right, StringComparison.InvariantCultureIgnoreCase))
            {
                Table.ChangeToysDirection(ToyAction.Right);
            }
        }
    }
}
