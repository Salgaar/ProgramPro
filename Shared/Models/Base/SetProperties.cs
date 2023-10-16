using ProgramPro.Shared.Attributes;
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
        [PositiveNumber(ErrorMessage = "Dette felt skal være positivt og større end 0.")]
        [Required]
        public double Weight { get; set; }
        [PositiveNumber(ErrorMessage = "Dette felt skal være positivt og større end 0.")]
        [Required]
        public int Reps { get; set; }
        public string RPE { get; set; }
        public string RIR { get; set; }
        public string PercentageOfOneRepMax { get; set; }
    }
}
