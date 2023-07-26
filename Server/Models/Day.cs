namespace ProgramPro.Server.Models
{
    public class Day
    {
        public int Id { get; set; }
        public int WeekId { get; set; }
        public Week Week { get; set; }
    }
}
