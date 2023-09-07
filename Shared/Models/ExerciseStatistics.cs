namespace ProgramPro.Shared.Models
{
    public class ExerciseStatistics
    {
        public int Id { get; set; }
        public int StatisticsId { get; set; }
        public Statistics Statistics { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public ICollection<PersonalRecord> PersonalRecords { get; set; }
    }
}
