using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyRobot.Logic.Models
{
    /// <summary>
    /// Representing the table for the game
    /// </summary>
    public static class Table
    {
        private const int Rows = 5;
        private const int Columns = 5;

        private static List<Position> Positions { get; set; }
        private static Toy Robot { get; set; }

        /// <summary>
        /// Instantiate Table data and Toy Robot
        /// </summary>
        static Table()
        {
            Positions = new List<Position>();

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    Positions.Add(new Position { X = x, Y = y, Status = Status.Open });
                }
            }

            // Set coordinates to null as Toy not yet placed
            Robot = new Toy { X = null, Y = null };
        }

        /// <summary>
        /// Return status of robot
        /// </summary>
        /// <returns></returns>
        public static string Report()
        {
            return Robot.ReportStatus();
        }

        /// <summary>
        /// Check given action, and switch cardinal direction
        /// </summary>
        /// <param name="toyAction"></param>
        public static void ChangeToysDirection(ToyAction toyAction)
        {
            if (toyAction.Equals(ToyAction.Left))
            {
                switch (Robot.ToyDirection)
                {
                    case ToyDirection.East:
                        Robot.ToyDirection = ToyDirection.North;
                        break;
                    case ToyDirection.North:
                        Robot.ToyDirection = ToyDirection.West;
                        break;
                    case ToyDirection.West:
                        Robot.ToyDirection = ToyDirection.South;
                        break;
                    case ToyDirection.South:
                        Robot.ToyDirection = ToyDirection.East;
                        break;
                }
            }

            if (toyAction.Equals(ToyAction.Right))
            {
                switch (Robot.ToyDirection)
                {
                    case ToyDirection.East:
                        Robot.ToyDirection = ToyDirection.South;
                        break;
                    case ToyDirection.North:
                        Robot.ToyDirection = ToyDirection.East;
                        break;
                    case ToyDirection.West:
                        Robot.ToyDirection = ToyDirection.North;
                        break;
                    case ToyDirection.South:
                        Robot.ToyDirection = ToyDirection.West;
                        break;
                }
            }
        }

        /// <summary>
        /// Check to see if a position on the table exists exists
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool PositionExists(int x, int y)
        {
            var position = Positions.FirstOrDefault(tile => tile.X == x && tile.Y == y);
            return position != null;
        }

        /// <summary>
        /// Place Toy on board with given direction
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="toyDirection"></param>
        public static void PlaceToy(int x, int y, ToyDirection toyDirection)
        {
            // Find where toy is
            int[] coordinatesOfToy = FindCoordinatesOfToy(Positions);

            if (coordinatesOfToy != null)
            {
                var currentX = coordinatesOfToy[0];
                var currentY = coordinatesOfToy[1];

                foreach (Position tile in Positions.Where(tile => tile.X == currentX && tile.Y == currentY))
                {
                    tile.Status = Status.Open;
                }
            }

            if (!PositionExists(x, y))
            {
                throw new ArgumentException("Tile does not exist");
            }

            foreach (Position tile in Positions.Where(tile => tile.X == x && tile.Y == y))
            {
                tile.Status = Status.Toy;
            }

            // Set coordinates and direction for toy
            Robot.X = x;
            Robot.Y = y;
            Robot.ToyDirection = toyDirection;
        }

        /// <summary>
        /// Move Toy across table
        /// </summary>
        /// <param name="toyAction"></param>
        public static void MoveToy(ToyAction toyAction)
        {
            int?[] position = GetToyCoordinates();

            int? x = position[0];
            int? y = position[1];

            if(x == null || y == null)
            {
                throw new ApplicationException("Toy not placed");
            }

            var direction = Robot.ToyDirection;

            if(toyAction == ToyAction.Move)
            {
                Position currentPosition;
                Position nextPosition;

                switch (direction)
                {
                    case ToyDirection.East:
                        nextPosition = Positions.FirstOrDefault(tile => tile.X == x + 1 && tile.Y == y);
                        currentPosition = Positions.FirstOrDefault(tile => tile.X == x && tile.Y == y);

                        if (nextPosition != null)
                        {
                            int currentTileLocation = Positions.IndexOf(currentPosition);
                            int nextTileLocation = Positions.IndexOf(nextPosition);

                            Positions[nextTileLocation].Status = Status.Toy;
                            Positions[currentTileLocation].Status = Status.Open;
                        }
                        break;
                    case ToyDirection.North:
                        nextPosition = Positions.FirstOrDefault(tile => tile.X == x && tile.Y == y + 1);
                        currentPosition = Positions.FirstOrDefault(tile => tile.X == x && tile.Y == y);

                        if (nextPosition != null)
                        {
                            int currentTileLocation = Positions.IndexOf(currentPosition);
                            int nextTileLocation = Positions.IndexOf(nextPosition);

                            Positions[nextTileLocation].Status = Status.Toy;
                            Positions[currentTileLocation].Status = Status.Open;
                        }
                        break;
                    case ToyDirection.West:
                        nextPosition = Positions.FirstOrDefault(tile => tile.X == x - 1 && tile.Y == y);
                        currentPosition = Positions.FirstOrDefault(tile => tile.X == x && tile.Y == y);

                        if (nextPosition != null)
                        {
                            int currentTileLocation = Positions.IndexOf(currentPosition);
                            int nextTileLocation = Positions.IndexOf(nextPosition);

                            Positions[nextTileLocation].Status = Status.Toy;
                            Positions[currentTileLocation].Status = Status.Open;
                        }
                        break;
                    case ToyDirection.South:
                        nextPosition = Positions.FirstOrDefault(tile => tile.X == x && tile.Y == y - 1);
                        currentPosition = Positions.FirstOrDefault(tile => tile.X == x && tile.Y == y);

                        if (nextPosition != null)
                        {
                            int currentTileLocation = Positions.IndexOf(currentPosition);
                            int nextTileLocation = Positions.IndexOf(nextPosition);

                            Positions[nextTileLocation].Status = Status.Toy;
                            Positions[currentTileLocation].Status = Status.Open;
                        }
                        break;
                }
            }
            
            // Update coordinates
            FindCoordinatesOfToy();
        }

        /// <summary>
        /// Obtains coordinate of toy as array
        /// </summary>
        /// <returns></returns>
        public static int?[] GetToyCoordinates()
        {
            return new[] { Robot.X, Robot.Y };
        }

        /// <summary>
        /// Obtains direction of toy
        /// </summary>
        /// <returns></returns>
        public static ToyDirection GetToyDirection()
        {
            return Robot.ToyDirection;
        }

        /// <summary>
        /// Sets coordinates for toy
        /// </summary>
        public static void FindCoordinatesOfToy()
        {
            var coordinates = FindCoordinatesOfToy(Positions);

            Robot.X = coordinates[0];
            Robot.Y = coordinates[1];
        }

        /// <summary>
        /// Returns the coordinates of the toy
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        private static int[] FindCoordinatesOfToy(IEnumerable<Position> coordinates)
        {
            Position position = coordinates.FirstOrDefault(tile => tile.Status == Status.Toy);

            return position != null ? new[] { position.X, position.Y } : null;
        }
    }
}
