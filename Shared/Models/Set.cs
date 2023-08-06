namespace ProgramPro.Shared.Models
{
    public class Set
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public Day Day { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public List<Entry> Entries { get; set; }
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
