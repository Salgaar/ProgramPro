namespace ProgramPro.Shared.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public Trainingprogram Program { get; set; }
    }
}
