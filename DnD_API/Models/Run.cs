namespace DnD_API.Models
{
    public class Run
    {
        public string? Id { get; set; }
        public Guid CharacterId { get; set; }
        public string Status { get; set; } = "in_progress";
        public string CurrentRoomId { get; set; } = "r1";
        public List<string> DiscoveredRoomIds { get; set; } = new();
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public int Seed { get; set; } = new Random().Next();
        public List<RunLogEntry> Log { get; set; } = new();
    }

    public class RunLogEntry
    {
        public DateTime Ts { get; set; }
        public string Event { get; set; }
        public int Roll { get; set; }
    }
}
