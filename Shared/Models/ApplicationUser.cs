using Microsoft.AspNetCore.Identity;

namespace ProgramPro.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<TrainingProgram> TrainingPrograms { get; set; }
    }
}