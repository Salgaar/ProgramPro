using System.ComponentModel.DataAnnotations;

namespace ProgramPro.Server.Models
{
    public class TrainingProgram
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
