using System.ComponentModel.DataAnnotations;

namespace ProgramPro.Shared.Models
{
    public class Goal : SetProperties
    {
        [Key]
        public int Id { get; set; }
        public Guid TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
