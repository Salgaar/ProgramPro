namespace ProgramPro.Shared.Models
{
    public class Day
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public Part Part { get; set; }
        public List<Workout> Workouts { get; set; }
        public DateTime Date { get; set; }
    }
}
