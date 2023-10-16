

namespace ProgramPro.Shared.Models
{
    public class BodyStatistics
    {
        public int Id { get; set; }
        public int StatisticsId { get; set; }
        public Statistics Statistics { get; set; }
        public double Weight { get; set; }
        public DateTime DateLogged { get; set; }
    }
}
