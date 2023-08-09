using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPro.Shared.Models
{
    public class PartDefinition
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<Part> Parts { get; set; }
        public List<Day> Days { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int AmountOfDays { get; set; }
    }
}
