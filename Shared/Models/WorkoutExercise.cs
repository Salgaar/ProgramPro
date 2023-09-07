using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPro.Shared.Models
{
    public class WorkoutExercise
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public Day Day { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
        public ICollection<Set> Sets { get; set; }
    }
}
