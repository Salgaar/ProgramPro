using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProgramPro.Shared.Models
{
    public class DayDefinition
    {
        public int Id { get; set; }
        public int ComponentDefinitionId { get; set; }
        public ComponentDefinition ComponentDefinition { get; set; }
        public ICollection<WorkoutExerciseDefinition> WorkoutExerciseDefinitions { get; set; }
        public int Number { get; set; }
        public DayType Type { get; set; } = DayType.Training;
    }
}
