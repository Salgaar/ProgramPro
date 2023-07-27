namespace ProgramPro.Shared.Models
{
    public class Week
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public Trainingprogram Program { get; set; }
        public List<Day> Days { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
