using Kolos2.DTO;
using Kolos2.Repository;

namespace Kolos2.Service;
public class CharacterService : ICharacterService
{
    private readonly ICharacterRepository _characterRepository;
    public CharacterService(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }
    public CharacterDTO GetCharacter(int characterId)
    {
        var character = _characterRepository.GetCharacter(characterId);
        if (character == null) return null;
        var characterDto = new CharacterDTO
        {
            FirstName = character.FirstName,
            LastName = character.LastName,
            CurrentWeight = character.CurrentWeight,
            MaxWeight = character.MaxWeight,
            BackpackItems = character.Backpacks.Select(b => new BackpackItemDTO
            {
                ItemName = b.Item.Name,
                ItemWeight = b.Item.Weight,
                Amount = b.Amount
            }).ToList(),
            Titles = character.CharacterTitles.Select(ct => new TitleDTO
            {
                Title = ct.Title.Name,
                AcquiredAt = ct.AcquiredAt
            }).ToList()
        };
        return characterDto;
    }
    public bool AddItemsToBackpack(int characterId, List<int> itemIds)
    {
        return _characterRepository.AddItemsToBackpack(characterId, itemIds);
    }
}