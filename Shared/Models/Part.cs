namespace ProgramPro.Shared.Models
{
    public class Part
    {
        public int Id { get; set; }
        public int TrainingprogramId { get; set; }
        public Trainingprogram Trainingprogram { get; set; }
        public List<Day> Days { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int AmountOfDays { get; set; }
    }
}
