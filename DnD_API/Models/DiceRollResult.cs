namespace DnD_API.Models
{
    public class DiceRollResult
    {
        public string Formula { get; set; }
        public int Result { get; set; }
        public List<int> Dice { get; set; } = new();
        public int Modifier { get; set; }
    }
}
