namespace ProgramPro.Shared.Models
{
    public class Day
    {
        public int Id { get; set; }
        public int PartDefinitionId { get; set; }
        public PartDefinition PartDefinition { get; set; }
        public List<Set> Set { get; set; }
        public int DayNumber { get; set; }
    }
}
