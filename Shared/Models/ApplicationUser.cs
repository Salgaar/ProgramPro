using Microsoft.AspNetCore.Identity;

namespace ProgramPro.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Statistics> Statistics { get; set; }
        public List<Trainingprogram> Programs { get; set; }
    }
}