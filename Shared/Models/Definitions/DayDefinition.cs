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
        public int SplitDefinitionId { get; set; }
        public SplitDefinition SplitDefinition { get; set; }
        public ICollection<WorkoutExerciseDefinition> WorkoutExerciseDefinitions { get; set; }
        public int Number { get; set; }
    }
}
