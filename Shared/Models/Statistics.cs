using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgramPro.Shared.Models
{
    public class Statistics
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<ExerciseStatistics> ExerciseStatistics { get; set; }
        public List<BodyStatistics> BodyStatistics { get; set; }
    }
}
