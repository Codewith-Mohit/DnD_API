using DnD_API.Models;

namespace DnD_API.Services
{
    public class DungeonService
    {
        // deterministic tiny dungeon generator per seed.. minimum viable implementation
        public Room GenerateInitialRoom(int seed)
        {
            // simple two-room chain
            return new Room
            {
                Id = "r1",
                Type = "enemy",
                Description = "A damp corridor with distant dripping.",
                Exits = new List<string> { "r2" },
                Encounter = new RoomEncounter
                {
                    Enemies = new List<Enemy> 
                    { 
                        new Enemy 
                        { 
                            Name = "Goblin", 
                            Hp = 7, 
                            Ac = 12, 
                            Attack = "+3", 
                            Damage = "-5" 
                        }
                    }
                }
            };            
        }
    }
}
