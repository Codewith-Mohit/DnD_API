namespace DnD_API.Models
{
    public class RunLogEntry
    {
        public DateTime Ts { get; set; }
        public string Event { get; set; }
        public DiceRollResult Roll { get; set; }
    }
}
