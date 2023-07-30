namespace ProgramPro.Shared.Models
{
    public class PersonalRecord
    {
        public int Id { get; set; }
        public int ExerciseStatisticsId { get; set; }
        public ExerciseStatistics ExerciseStatistics { get; set; }
        public DateTime DateLogged { get; set; }
        public double Weight { get; set; }
        public int Reps { get; set; }
        public double RPE { get; set; }
        public int RIR { get; set; }
        public int PercentageOfOneRepMax { get; set; }
    }
}
