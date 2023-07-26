namespace ProgramPro.Server.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public TrainingProgram Program { get; set; }
    }
}
