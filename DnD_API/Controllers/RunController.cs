using DnD_API.Dtos;
using DnD_API.Models;
using DnD_API.Services;
using DnD_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DnD_API.Controllers
{
    [ApiController]  
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
        [Route("runs")]
        public IActionResult CreateRun([FromBody] RunCreateDto dto)
        {
            var run = _runs.CreateRun(dto.CharacterId, dto.Seed);
            return CreatedAtAction(nameof(GetRun), new { id = run.Id }, run);
        }

        [HttpGet("{id}")]
        public IActionResult GetRun(string id)
        {
            var r = _runStore.Get(id);
            return r != null ? Ok($"Player :{r.CharacterId} is active with Run {r.Id} in Room {r.CurrentRoomId}") : NotFound();
        }

        [HttpPost("{id}/explore")]
        public IActionResult Explore(string id, RunExploreRequest req)
        {
            var result = _runs.Explore(id);
            if (result.IsError) NotFound($"{result.ErrorMessage}");

            dynamic data = result.Data;
            return Ok( $"Exploring through {data.runId} and moving to next room {data.currentRoom}");
        }

        [HttpPost("{id}/encounter/roll")]
        public IActionResult ResolveEncounter(string id, DiceRollRequest req, RunService runService)
        {
            var result = runService.ResolveEncounter(id, req);
            if (result.IsError) return NotFound(result.Msgs);
            return Ok($"{result.Msgs}");
        }

        [HttpPost("{id}/encounter/flee")]
        public IActionResult Flee(string id, RunService runService)
        {
            var res = runService.Flee(id);
            return Ok("Not Implemented Yet...");
        }

        //abort
        [HttpPost("{id}/abort")]
        public IActionResult AbortRun(string id, RunService runService)
        {
            var result = runService.Abort(id);
            return Ok("Not Implemented Yet...");
        }
    }
}
