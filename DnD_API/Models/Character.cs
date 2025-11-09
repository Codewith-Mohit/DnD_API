namespace DnD_API.Models
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
        public List<Item> Inventory { get; set; } = new();
    }
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // e.g., "weapon"
        public int Bonus { get; set; }
    }
}
