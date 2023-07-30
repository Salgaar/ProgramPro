namespace ProgramPro.Shared.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public Trainingprogram Program { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public double Weight { get; set; }
        public int Reps { get; set; }
        public double RPE { get; set; }
        public int RIR { get; set; }
        public int PercentageOfOneRepMax { get; set; }
    }
}
