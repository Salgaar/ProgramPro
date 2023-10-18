using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPro.Shared.Models.DataTransferObjects
{
    public class OverwriteDay
    {
        public Day ToDelete { get; set; }
        public Day ToAdd { get; set; }
    }
}
