using DnD_API.Services;
using DnD_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DnD_API.Controllers
{
    [ApiController]
    [Route("dice")]
    public class DiceController : ControllerBase
    {
        private readonly IDiceService _diceService;
        public DiceController(IDiceService diceService)
        {
            _diceService = diceService;
        }
        [HttpPost("roll")]
        public IActionResult Roll([FromBody] DiceRequest req)
        {
            var result = _diceService.Roll(req.Formula, req.Seed);
            return Ok(new { req.Formula, req.Seed, result });
        }

        public record DiceRequest(string Formula, int? Seed);
    }

}
