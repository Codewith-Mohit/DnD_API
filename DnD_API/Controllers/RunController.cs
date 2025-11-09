using DnD_API.Dtos;
using DnD_API.Models;
using DnD_API.Services;
using DnD_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DnD_API.Controllers
{
    [ApiController]
    [Route("runs")]
    public class RunsController : ControllerBase
    {
        private readonly RunService _runs;
        private readonly IRunStore _runStore;
        public RunsController(RunService runs, IRunStore runStore)
        {
            _runs = runs;
            _runStore = runStore;
        }

        [HttpPost]
        public IActionResult CreateRun([FromBody] RunCreateDto dto)
        {
            var run = _runs.CreateRun(dto.CharacterId, dto.Seed);
            return CreatedAtAction(nameof(GetRun), new { id = run.Id }, run);
        }

        [HttpGet("{id}")]
        public IActionResult GetRun(string id)
        {
            var r = _runStore.Get(id);
            return r != null ? Ok(r) : NotFound();
        }

        [HttpPost]
        [Route("start")]
        public IActionResult StartRun(RunCreateDto dto, IRunStore runStore, ICharacterServices charStore, DungeonService dungeon)
        {
            var character = charStore.GetCharacter(dto.CharacterId);
            if (character == null)
                throw new ArgumentException("Invalid character ID");


            var run = new Run
            {
                Id = Guid.NewGuid().ToString(),
                CharacterId = dto.CharacterId,
                StartedAt = DateTime.UtcNow,
                Status = "In_progress",
                CurrentRoomId = null,
                DiscoveredRoomIds = new List<string>(),
                Seed = dto.Seed ?? new Random().Next(),
                Log = new List<RunLogEntry>
                {
                    new RunLogEntry { Ts = DateTime.UtcNow, Event = $"Run started for character {character.Name}" }
                }
            };

            var firstRoom = dungeon.GenerateInitialRoom(run.Seed);
            run.CurrentRoomId = firstRoom.Id;
            run.DiscoveredRoomIds.Add(firstRoom.Id);
            run.Log.Add(new RunLogEntry { Ts = DateTime.UtcNow, Event = $"Started run in room {firstRoom.Id}" });

            runStore.Create(run);
            return CreatedAtAction(nameof(GetRun), new { id = run.Id }, run);
        }

        [HttpPost("{id}/explore")]
        public IActionResult Explore(string id, RunExploreRequest req)
        {
            var result = _runs.Explore(id);
            if (result.IsError) NotFound(result.ErrorMessage);

            return Ok(result);
        }

        [HttpPost("{id}/encounter/roll")]
        public IActionResult ResolveEncounter(string id, DiceRollRequest req, RunService runService)
        {
            var result = runService.ResolveEncounter(id, req);
            if (result.IsError) return NotFound(result.ErrorMessage);
            return Ok(result);
        }

        [HttpPost("{id}/encounter/flee")]
        public IActionResult Flee(string id, RunService runService)
        {
            var res = runService.Flee(id);
            if (res) return BadRequest(res);
            return Ok(res);
        }

        //abort
        [HttpPost("{id}/abort")]
        public IActionResult AbortRun(string id, RunService runService)
        {
            var result = runService.Abort(id);
            if (result) return NotFound(result);
            return Ok(result);

        }
    }
}
