using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoneProject.Logic.Models
{

    public static class Table
    {
        private const int Rows = 5;
        private const int Columns = 5;

        private static List<Position> Data { get; set; }
        private static Toy Robot { get; set; }

        static Table()
        {
            Data = new List<Position>();

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Columns; y++)
                {
                    Data.Add(new Position { X = x, Y = y, Status = Status.Open });
                }
            }

            Robot = new Toy { X = null, Y = null };
        }

        public static string Report()
        {
            return Robot.ReportStatus();
        }

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

        public static bool TileExists(int x, int y)
        {
            List<Position> tiles = Data
                .Where(tile => tile.X == x && tile.Y == y)
                .ToList();

            return tiles.Count != 0;
        }

        public static void PlaceToy(int x, int y, ToyDirection toyDirection)
        {
            // Find where toy is
            int[] coordinatesOfToy = FindCoordinatesOfToy(Data);

            if (coordinatesOfToy != null)
            {
                var currentX = coordinatesOfToy[0];
                var currentY = coordinatesOfToy[1];

                foreach (Position tile in Data.Where(tile => tile.X == currentX && tile.Y == currentY))
                {
                    tile.Status = Status.Open;
                }
            }

            if (!TileExists(x, y))
            {
                throw new ArgumentException("Tile does not exist");
            };

            foreach (Position tile in Data.Where(tile => tile.X == x && tile.Y == y))
            {
                tile.Status = Status.Toy;
            }

            Robot.X = x;
            Robot.Y = y;
            Robot.ToyDirection = toyDirection;
        }

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
                        nextPosition = Data.FirstOrDefault(tile => tile.X == x + 1 && tile.Y == y);
                        currentPosition = Data.FirstOrDefault(tile => tile.X == x && tile.Y == y);

                        if (nextPosition != null)
                        {
                            int currentTileLocation = Data.IndexOf(currentPosition);
                            int nextTileLocation = Data.IndexOf(nextPosition);

                            Data[nextTileLocation].Status = Status.Toy;
                            Data[currentTileLocation].Status = Status.Open;
                        }
                        break;
                    case ToyDirection.North:
                        nextPosition = Data.FirstOrDefault(tile => tile.X == x && tile.Y == y + 1);
                        currentPosition = Data.FirstOrDefault(tile => tile.X == x && tile.Y == y);

                        if (nextPosition != null)
                        {
                            int currentTileLocation = Data.IndexOf(currentPosition);
                            int nextTileLocation = Data.IndexOf(nextPosition);

                            Data[nextTileLocation].Status = Status.Toy;
                            Data[currentTileLocation].Status = Status.Open;
                        }
                        break;
                    case ToyDirection.West:
                        nextPosition = Data.FirstOrDefault(tile => tile.X == x - 1 && tile.Y == y);
                        currentPosition = Data.FirstOrDefault(tile => tile.X == x && tile.Y == y);

                        if (nextPosition != null)
                        {
                            int currentTileLocation = Data.IndexOf(currentPosition);
                            int nextTileLocation = Data.IndexOf(nextPosition);

                            Data[nextTileLocation].Status = Status.Toy;
                            Data[currentTileLocation].Status = Status.Open;
                        }
                        break;
                    case ToyDirection.South:
                        nextPosition = Data.FirstOrDefault(tile => tile.X == x && tile.Y == y - 1);
                        currentPosition = Data.FirstOrDefault(tile => tile.X == x && tile.Y == y);

                        if (nextPosition != null)
                        {
                            int currentTileLocation = Data.IndexOf(currentPosition);
                            int nextTileLocation = Data.IndexOf(nextPosition);

                            Data[nextTileLocation].Status = Status.Toy;
                            Data[currentTileLocation].Status = Status.Open;
                        }
                        break;
                }
            }
            
            // Update coordinates
            FindCoordinatesOfToy();
        }

        public static int?[] GetToyCoordinates()
        {
            return new[] { Robot.X, Robot.Y };
        }

        public static ToyDirection GetToyDirection()
        {
            return Robot.ToyDirection;
        }

        public static void FindCoordinatesOfToy()
        {
            var test = FindCoordinatesOfToy(Data);

            Robot.X = test[0];
            Robot.Y = test[1];
        }

        private static int[] FindCoordinatesOfToy(IEnumerable<Position> coordinates)
        {
            Position test = coordinates.FirstOrDefault(tile => tile.Status == Status.Toy);

            return test != null ? new[] { test.X, test.Y } : null;
        }
    }
}
