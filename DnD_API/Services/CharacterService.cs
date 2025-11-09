using DnD_API.Data;
using DnD_API.Models;
using DnD_API.Services.Interfaces;

namespace DnD_API.Services
{
    public class CharacterService : ICharacterServices
    {
        private readonly DnDDbContext _dbContext;

        public CharacterService(DnDDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Character? GetCharacter(Guid id)
        {
            return _dbContext.Characters.Find(id);
        }

        public Character CreateCharacter(Character character)
        {
            character.Id = Guid.NewGuid();
            _dbContext.Characters.Add(character);
            _dbContext.SaveChanges();
            return character;
        }

        public void DeleteCharacter(Guid characterId)
        {
            var character = _dbContext.Characters?.Find(characterId);
            _dbContext.Characters?.Remove(character);
            _dbContext.SaveChanges();
        }
    }
}
