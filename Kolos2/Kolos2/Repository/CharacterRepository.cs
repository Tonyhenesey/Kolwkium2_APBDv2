using Kolokwium2.Models;
using Kolos2.DbContext;
using Kolos2.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolos2.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly MyDbContext _context;

        public CharacterRepository(MyDbContext context)
        {
            _context = context;
        }

        public Character GetCharacter(int characterId)
        {
            return _context.Characters
                .Include(c => c.Backpacks)
                .ThenInclude(b => b.Item)
                .Include(c => c.CharacterTitles)
                .ThenInclude(ct => ct.Title)
                .FirstOrDefault(c => c.Id == characterId);
        }

        public bool AddItemsToBackpack(int characterId, List<int> itemIds)
        {
            var character = _context.Characters.Include(c => c.Backpacks).FirstOrDefault(c => c.Id == characterId);
            if (character == null) return false;

            var items = _context.Items.Where(i => itemIds.Contains(i.Id)).ToList();
            if (items.Count != itemIds.Count) return false; // Ensure all items exist

            int totalNewWeight = items.Sum(i => i.Weight);

            if (character.CurrentWeight + totalNewWeight > character.MaxWeight)
                return false; // Ensure character has enough capacity

            foreach (var item in items)
            {
                var backpackItem = character.Backpacks.FirstOrDefault(b => b.ItemId == item.Id);
                if (backpackItem == null)
                {
                    character.Backpacks.Add(new Backpack { CharacterId = characterId, ItemId = item.Id, Amount = 1 });
                }
                else
                {
                    backpackItem.Amount++;
                }
                character.CurrentWeight += item.Weight;
            }
            _context.SaveChanges();
            return true;
        }
    }
}