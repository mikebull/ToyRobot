using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobot.Logic;
using ToyRobot.Logic.Models;

namespace ToyRobot.Test
{
    /// <summary>
    /// Test key functionality of table
    /// </summary>
    [TestClass]
    public class TableTest
    {
        /// <summary>
        /// Check to see if an exception is raised when
        /// the toy is placed off of the table
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PlacedOffTable()
        {
            ParseCommands.Place("PLACE 120,120,NORTH");
        }

        /// <summary>
        /// Check to see if an exception is thrown if
        /// the toy is given an incorrect direction
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IncorrectDirection()
        {
            ParseCommands.Place("PLACE 0,0,NORTH-EAST");
        }

        /// <summary>
        /// Moves toy to see if toy placed
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void ToyNotPlaced()
        {
            ParseCommands.Move("Move");
        }

        /// <summary>
        /// Check to see if robot can move off of table
        /// </summary>
        [TestMethod]
        public void ShouldNotMoveIfOffTable()
        {
            ParseCommands.Place("Place 0,0,South");
            ParseCommands.Move("Move");

            var coordinates = Table.GetToyCoordinates();
            int? x = coordinates[0];
            int? y = coordinates[1];

            Assert.AreEqual(x, 0);
            Assert.AreEqual(y, 0);
        }

        /// <summary>
        /// Check that toy rotate correctly
        /// </summary>
        [TestMethod]
        public void ToyRotate()
        {
            ParseCommands.Place("Place 0, 0,North");

            ParseCommands.Direction("RIGHT");
            ToyDirection direction = Table.GetToyDirection();
            Assert.AreEqual(ToyDirection.East, direction);

            ParseCommands.Direction("RIGHT");
            direction = Table.GetToyDirection();
            Assert.AreEqual(ToyDirection.South, direction);

            ParseCommands.Direction("RIGHT");
            direction = Table.GetToyDirection();
            Assert.AreEqual(ToyDirection.West, direction);

            ParseCommands.Direction("RIGHT");
            direction = Table.GetToyDirection();
            Assert.AreEqual(ToyDirection.North, direction);
        }
    }
}
