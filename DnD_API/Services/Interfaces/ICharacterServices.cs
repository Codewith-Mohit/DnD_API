using DnD_API.Models;

namespace DnD_API.Services.Interfaces
{
    public interface ICharacterServices
    {
        Character? CreateCharacter(Character c);
        Character? GetCharacter(Guid id);
        void DeleteCharacter(Guid id);
    }
}
