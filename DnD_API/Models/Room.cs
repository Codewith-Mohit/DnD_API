namespace DnD_API.Models
{
    public class Room
    {
        public string Id { get; set; }
        public string Type { get; set; } // "enemy" | "treasure" | "trap" | "empty" | "boss"
        public string Description { get; set; }
        public List<string> Exits { get; set; } = new();
        public RoomEncounter Encounter { get; set; }
    }
    public class RoomEncounter
    {
        public List<Enemy> Enemies { get; set; } = new();
    }

}
