namespace ProgramPro.Server.Models
{
    public class Week
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public TrainingProgram Program { get; set; }
    }
}
