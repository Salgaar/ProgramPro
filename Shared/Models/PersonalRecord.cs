namespace ProgramPro.Shared.Models
{
    public class PersonalRecord : SetProperties
    {
        public int Id { get; set; }
        public int ExerciseStatisticsId { get; set; }
        public ExerciseStatistics ExerciseStatistics { get; set; }
        public DateTime DateLogged { get; set; }
    }
}
