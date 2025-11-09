namespace DnD_API.Models
{
    public class Enemy
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Ac { get; set; }
        public string Attack { get; set; } // e.g., "+3"
        public string Damage { get; set; } // e.g., "1d6"
    }
}
