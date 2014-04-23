using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZoneProject.Logic.Models;

namespace ZoneProject.Test
{
    /// <summary>
    /// Test data to show working application
    /// </summary>
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void ExampleOne()
        {
            ParseCommands.Place("PLACE 0,0,NORTH");
            ParseCommands.Move("MOVE");
            string report = Table.Report();

            Assert.AreEqual(report, "0,1,NORTH");
        }

        [TestMethod]
        public void ExampleTwo()
        {
            ParseCommands.Place("PLACE 0,0,NORTH");
            ParseCommands.Direction("LEFT");
            string report = Table.Report();

            Assert.AreEqual(report, "0,0,WEST");
        }

        [TestMethod]
        public void ExampleThree()
        {
            ParseCommands.Place("PLACE 1,2,EAST");
            ParseCommands.Move("MOVE");
            ParseCommands.Move("MOVE");
            ParseCommands.Direction("LEFT");
            ParseCommands.Move("MOVE");
            string report = Table.Report();

            Assert.AreEqual(report, "3,3,NORTH");
        }
    }
}
