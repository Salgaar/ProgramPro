using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgramPro.Shared.Models
{
    public class Statistics
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<ExerciseStatistics> ExerciseStatistics { get; set; }
        public ICollection<BodyStatistics> BodyStatistics { get; set; }
    }
}
