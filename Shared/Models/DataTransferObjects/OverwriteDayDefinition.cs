using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPro.Shared.Models.DataTransferObjects
{
    public class OverwriteDayDefinition
    {
        public DayDefinition ToDelete { get; set; }
        public DayDefinition ToAdd { get; set; }
    }
}
