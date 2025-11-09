using DnD_API.Models;

namespace DnD_API.Dtos
{
    public class CharacterCreateDto
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; } = 1;
        public int Hp { get; set; } = 20;
        public int Strength { get; set; } = 10;
        public int Dexterity { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public List<Item> Inventory { get; set; }
    }

}
