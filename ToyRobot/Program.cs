using System;
using System.Collections.Generic;
using ToyRobot.Logic;
using ToyRobot.Logic.Models;

namespace ToyRobot
{
    public class Program
    {
        /// <summary>
        /// Command delegate for parsed commands
        /// </summary>
        /// <param name="input"></param>
        public delegate void Command(string input);

        /// <summary>
        /// Return a list of commands ending with report 
        /// from the command line
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<string> GetCommands()
        {
            // Read in standard input
            var input = new List<string>();

            string line = Console.ReadLine();
            while (!String.Equals(line, Constants.Commands.Report, StringComparison.InvariantCultureIgnoreCase))
            {
                input.Add(line);
                line = Console.ReadLine();
            }

            return input;
        }

        /// <summary>
        /// Parse string commands and build set of delegates
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        private static IEnumerable<Tuple<string, Command>> ParseCommandsToInstructions(IEnumerable<string> commands)
        {
            var instructions = new List<Tuple<string, Command>>();

            foreach (string command in commands)
            {
                if (command.Contains(Constants.Commands.Place, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Place command
                    instructions.Add(new Tuple<string, Command>(command, ParseCommands.Place));
                }

                if (command.Contains(Constants.Commands.Move, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Move command
                    instructions.Add(new Tuple<string, Command>(command, ParseCommands.Move));
                }

                if (command.Contains(Constants.Commands.Left, StringComparison.InvariantCultureIgnoreCase) || command.Contains(Constants.Commands.Right, StringComparison.InvariantCultureIgnoreCase))
                {
                    // ToyDirection command
                    instructions.Add(new Tuple<string, Command>(command, ParseCommands.Direction));
                }
            }

            return instructions;
        }

        /// <summary>
        /// Main method for application
        /// </summary>
        private static void Main()
        {
            var retry = true;
            while (retry)
            {
                // Greet user
                Console.WriteLine("Please enter your commands");
                Console.WriteLine();

                // Read in standard input
                var input = GetCommands();
                var instructions = ParseCommandsToInstructions(input);

                // Feed instructions into delegate
                foreach (var instruction in instructions)
                {
                    Command applicationCommand = instruction.Item2;
                    applicationCommand(instruction.Item1);
                }

                // Print result
                Console.WriteLine();
                Console.WriteLine(Table.Report());

                Console.WriteLine();
                Console.WriteLine("Press the ENTER key to try again, and the ESC key to exit");
                Console.WriteLine();

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    retry = false;
                }
            }
        }
    }
}
