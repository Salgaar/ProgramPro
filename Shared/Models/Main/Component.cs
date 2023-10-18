using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPro.Shared.Models
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TrainingProgramId { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public int ComponentNumber { get; set; }
        public ICollection<Day> Days { get; set; }
    }
}
