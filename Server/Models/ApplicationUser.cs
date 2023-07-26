using Microsoft.AspNetCore.Identity;

namespace ProgramPro.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Statistics> Statistics { get; set; }
        public List<TrainingProgram> Programs { get; set; }
    }
}