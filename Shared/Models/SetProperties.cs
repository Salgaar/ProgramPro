using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPro.Shared.Models
{
    public class SetProperties
    {
        [Required]
        public double Weight { get; set; }
        [Required]
        public int Reps { get; set; }
        public string RPE { get; set; }
        public string RIR { get; set; }
        public string PercentageOfOneRepMax { get; set; }
    }
}
