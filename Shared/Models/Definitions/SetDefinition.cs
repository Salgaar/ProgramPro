using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPro.Shared.Models
{
    public class SetDefinition : SetProperties
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int WorkoutExerciseDefinitionId { get; set; }
        public WorkoutExerciseDefinition WorkoutExerciseDefinition { get; set; }
        public bool UsingRPE { get; set; } = false;
        public bool UsingRIR { get; set; } = false;
        public bool UsingPercentageOfOneRepMax { get; set; } = false;
        public SetType Type { get; set; }
    }
}
