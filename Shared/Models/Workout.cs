namespace ProgramPro.Shared.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public Day Day { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public List<Set> Set { get; set; }
    }
}
