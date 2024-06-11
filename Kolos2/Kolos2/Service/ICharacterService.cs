using Kolos2.DTO;
using Kolos2.Models;

namespace Kolos2.Service;

public interface ICharacterService
{
    CharacterDTO GetCharacter(int characterId);
    bool AddItemsToBackpack(int characterId, List<int> itemIds);
}
