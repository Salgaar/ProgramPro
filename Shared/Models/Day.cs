namespace ProgramPro.Shared.Models
{
    public class Day
    {
        public int Id { get; set; }
        public int WeekId { get; set; }
        public Week Week { get; set; }
        public List<Workout> Workouts { get; set; }
        public DateTime Date { get; set; }
    }
}
