using Kolos2.Models;

namespace Kolos2.Repository;

public interface ICharacterRepository
{
    Character GetCharacter(int characterId);
    bool AddItemsToBackpack(int characterId, List<int> itemIds);
}