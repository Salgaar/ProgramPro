using System.ComponentModel.DataAnnotations;

namespace ProgramPro.Shared.Models
{
    public class TrainingProgram
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<Day> Days { get; set; }
    }
}
