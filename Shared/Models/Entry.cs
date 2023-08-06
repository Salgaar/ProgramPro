namespace ProgramPro.Shared.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public Set Set { get; set; }
        public int ProgramId { get; set; }
        public Trainingprogram Program { get; set; }
        public double Weight { get; set; }
        public int Reps { get; set; }
        public double RPE { get; set; }
        public int RIR { get; set; }
        public int PercentageOfOneRepMax { get; set; }
    }
}
