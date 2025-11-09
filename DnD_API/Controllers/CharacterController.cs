using DnD_API.Dtos;
using DnD_API.Models;
using DnD_API.Services;
using DnD_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DnD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        //private readonly ILogger<CharacterController> _logger;
        private readonly ICharacterServices _CharService;

        public CharacterController(ICharacterServices CharService)
        {
            _CharService = CharService; 

        }

        [HttpGet("{id}")]
        public IActionResult GetCharacterById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid character ID.");
            }   
            var character = _CharService.GetCharacter(id);  

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);   
        }

        [HttpPost]
        public IActionResult CreateCharacter([FromBody] CharacterCreateDto dto)
        {

            var newCharacter = new Character
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Class = dto.Class,
                Level = dto.Level,
                Hp = dto.Hp,
                Strength = dto.Strength,
                Dexterity = dto.Dexterity,
                Intelligence = dto.Intelligence,
                 Inventory = dto.Inventory
            };
            
            _CharService.CreateCharacter(newCharacter);

            return CreatedAtAction(nameof(GetCharacterById), new { id = newCharacter.Id }, newCharacter);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCharacter(Guid id)
        {
            var character = _CharService.GetCharacter(id);
            if (character == null)
            {
                return NotFound();
            }
            _CharService.DeleteCharacter(id);
            return NoContent();
        }

    }
}
