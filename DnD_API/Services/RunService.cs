using DnD_API.Dtos;
using DnD_API.Models;
using DnD_API.Services.Interfaces;

namespace DnD_API.Services
{
    public class RunService
    {
        private readonly IDiceService _dice;
        private readonly IRunStore _store;
        private readonly DungeonService _dungeon;

        public RunService(IDiceService dice, IRunStore store, DungeonService dungeon)
        {
            _dice = dice;
            _store = store;
            _dungeon = dungeon;
        }

        public Run CreateRun(Guid characterId, int? seed)
        {
            var run = new Run
            {
                Id = Guid.NewGuid().ToString(),
                CharacterId = characterId,
                StartedAt = DateTime.UtcNow,
                Status = "in_progress",
                CurrentRoomId = null,
                DiscoveredRoomIds = new List<string>(),
                Seed = seed ?? new Random().Next(),
                Log = new List<RunLogEntry>()
            };

            var first = _dungeon.GenerateInitialRoom(run.Seed);
            run.CurrentRoomId = first.Id;
            run.DiscoveredRoomIds.Add(first.Id);
            run.Log.Add(new RunLogEntry { Ts = DateTime.UtcNow, Event = $"Started in {first.Id}" });

            _store.Create(run);
            return run;
        }

        public (bool IsError, string ErrorMessage, object Data) Explore(string runId)
        {
            var run = _store.Get(runId);
            if (run == null) return (true, "Run not found", null);

            //move to next
            if (run.CurrentRoomId == "r1")
            {
                var next = "r2";
                run.CurrentRoomId = next;
                run.DiscoveredRoomIds.Add(next);
                run.Log.Add(new RunLogEntry { Ts = DateTime.UtcNow, Event = $"Entered new room {next}" });
                _store.Update(run);
                return (false, null, new { runId = run.Id, currentRoom = next });
            }

            // no more rooms
            return (true, "No more rooms to explore", null);
        }

        public (bool IsError, string ErrorMessage, object Data) ResolveEncounter(string runId, DiceRollRequest req)
        {
            var run = _store.Get(runId);
            if (run == null) return (true, "Run not found", null);

            // Very simple: resolve against first enemy in current room
            // In a real app, look up room data; here we mock
            var enemy = new Enemy { Name = "Goblin", Hp = 7, Ac = 35, Attack = "+3", Damage = "-5" };

            var roll = _dice.Roll(req.Formula ?? "1d20", req.Seed);

            int totalHit = roll;
            bool hit = totalHit >= enemy.Ac;
            run.Log.Add(new RunLogEntry { Ts = DateTime.UtcNow, Event = $"Encounter roll: {roll}", Roll = roll });
            
            // simple HP deduction
            if (hit)            
                enemy.Hp -= Math.Max(1, (roll));            

            run.Log.Add(new RunLogEntry { Ts = DateTime.UtcNow, Event = $"Encounter roll: {req.Formula}", Roll = roll });

            // Update run status if enemy HP <= 0
            if (enemy.Hp <= 0)
            {
                run.Status = "victory";
                _store.Update(run);
            }

            _store.Update(run);
            run.Log.Add(new RunLogEntry { Ts = DateTime.UtcNow, Event = $": {req.Formula}", Roll = roll });

            return (false, null, new { runId = run.Id, hit, enemyHp = enemy.Hp });
        }

        public bool Flee(string runId) {  return true; }
        public bool Abort(string runId) { return true; }
    }
}