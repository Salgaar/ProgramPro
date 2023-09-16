namespace ProgramPro.Shared.Models
{
    public class Goal : SetProperties
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public TrainingProgram Program { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
