using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPro.Shared.Models
{
    public class WorkoutExerciseDefinition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int DayDefinitionId { get; set; }
        public DayDefinition DayDefinition { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public ICollection<SetDefinition> SetDefinitions { get; set; }
    }
}
