namespace ProgramPro.Shared.Models
{
    public class Part
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public Trainingprogram Program { get; set; }
        public int PartDefinitionId { get; set; }
        public PartDefinition PartDefinition { get; set; }
        public DateTime StartDate { get; set; }
    }
}
