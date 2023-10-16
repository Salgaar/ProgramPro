using System.ComponentModel.DataAnnotations;

namespace ProgramPro.Shared.Models
{
    public class Entry : SetProperties
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SetId { get; set; }
        public Set Set { get; set; }

    }
}
