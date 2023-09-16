using System.ComponentModel.DataAnnotations;

namespace ProgramPro.Shared.Models
{
    public class Set
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int WorkoutExerciseId { get; set; }
        public WorkoutExercise WorkoutExercise { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public int Reps { get; set; }
        public bool UsingRPE { get; set; } = false;
        public double RPE { get; set; }
        public bool UsingRIR { get; set; } = false;
        public int RIR { get; set; }
        public bool UsingPercentageOfOneRepMax { get; set; } = false;
        public int PercentageOfOneRepMax { get; set; }
        public SetType Type { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
    public enum SetType
    {
        None,
        Top,
        Heavy,
        Backoff
    }
}
