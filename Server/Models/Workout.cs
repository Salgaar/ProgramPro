namespace ProgramPro.Server.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public Day Day { get; set; }
    }
}
