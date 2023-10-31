using System.ComponentModel.DataAnnotations;

namespace ProgramPro.Shared.Models
{
    public class Set : SetProperties
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int WorkoutExerciseId { get; set; }
        public WorkoutExercise WorkoutExercise { get; set; }
        public bool UsingRPE { get; set; } = false;
        public bool UsingRIR { get; set; } = false;
        public bool UsingPercentageOfOneRepMax { get; set; } = false;
        public SetType Type { get; set; }
        public Entry Entry { get; set; }
    }
    public enum SetType
    {
        None,
        Top,
        Heavy,
        Backoff
    }
}
