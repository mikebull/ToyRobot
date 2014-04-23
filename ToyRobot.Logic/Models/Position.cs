namespace ZoneProject.Logic.Models
{
    /// <summary>
    /// Basic model for a given position on the table
    /// </summary>
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Status Status { get; set; }
    }
}
