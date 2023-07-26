namespace ProgramPro.Server.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
