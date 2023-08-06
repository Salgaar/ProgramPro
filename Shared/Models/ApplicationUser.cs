using Microsoft.AspNetCore.Identity;

namespace ProgramPro.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<PartDefinition> PartDefinitions { get; set; }
        public List<Statistics> Statistics { get; set; }
        public List<Trainingprogram> Programs { get; set; }
    }
}