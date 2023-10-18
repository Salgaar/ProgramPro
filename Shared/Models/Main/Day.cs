using System.ComponentModel.DataAnnotations;

namespace ProgramPro.Shared.Models
{
    public class Day
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ComponentId { get; set; }
        public Component Component { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
        public DateTime Date { get; set; }
        public DayType Type { get; set; } = DayType.Training;
    }
    public enum DayType
    {
        Training,
        Rest
    }
}
