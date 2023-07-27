namespace ProgramPro.Shared.Models
{
    public class Set
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public Entry Entry { get; set; }
        public double Weight { get; set; }
        public int Reps { get; set; }
        public double RPE { get; set; }
        public int RIR { get; set; }
        public int PercentageOfOneRepMax { get; set; }
        public SetType Type { get; set; }
    }
    public enum SetType
    {
        Top,
        Heavy,
        Backoff
    }
}
