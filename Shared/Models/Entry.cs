namespace ProgramPro.Shared.Models
{
    public class Entry : SetProperties
    {
        public int Id { get; set; }
        public int SetId { get; set; }
        public Set Set { get; set; }

    }
}
