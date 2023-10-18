using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPro.Shared.Models
{
    public class ComponentDefinition
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<DayDefinition> DayDefinitions { get; set; }
    }
}
