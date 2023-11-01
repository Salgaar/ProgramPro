using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPro.Shared.Models.DataVisualizer
{
    public class SetVisualizer
    {
        public Set Set {  get; set; }
        public DateTime Date { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public Exercise Exercise { get; set; }
    }
}

