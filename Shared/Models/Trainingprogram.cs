using System.ComponentModel.DataAnnotations;

namespace ProgramPro.Shared.Models
{
    public class Trainingprogram
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<Goal> Goals { get; set; }
        public List<Part> Parts { get; set; }
        public List<Entry> Entries { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
