namespace ProgramPro.Shared.Models
{
    public class Day
    {
        public int Id { get; set; }
        public int TrainingprogramId { get; set; }
        public TrainingProgram Trainingprogram { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
        public DateTime Date { get; set; }
    }
}
