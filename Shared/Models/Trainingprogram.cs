using System.ComponentModel.DataAnnotations;

namespace ProgramPro.Shared.Models
{
    public class Trainingprogram
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Result Result { get; set; }
        public Goal Goal { get; set; }
        public List<Week> Weeks { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
