namespace DnD_API.Dtos
{
    public class DiceRollRequest
    {
        public string Formula { get; set; } // e.g., "2d6+1"
        public int? Seed { get; set; }
    }
}
