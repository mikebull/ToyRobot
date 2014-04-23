using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoneProject.Logic;

namespace ZoneProject.Logic.Models
{

    public class Toy
    {
        public int? X { get; set; }
        public int? Y { get; set; }

        public ToyDirection ToyDirection { get; set; }

        public string ReportStatus()
        {
            return String.Format("{0},{1},{2}", X, Y, Convert.ToString(ToyDirection).ToUpper());
        }
    }
}
