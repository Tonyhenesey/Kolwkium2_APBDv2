using Kolos2.DTO;
using Kolos2.Service;
using Microsoft.AspNetCore.Mvc;

namespace Kolos2.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly ICharacterService _characterService;
    public CharactersController(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    [HttpGet("{characterId}")]
    public ActionResult<CharacterDTO> GetCharacter(int characterId)
    {
        var character = _characterService.GetCharacter(characterId);
        if (character == null)
        {
            return NotFound();
        }
        return Ok(character);
    }
    [HttpPost("{characterId}/backpacks")]
    public ActionResult AddItemsToBackpack(int characterId, [FromBody] List<int> itemIds)
    {
        if (!_characterService.AddItemsToBackpack(characterId, itemIds))
        {
            return BadRequest();
        }
        return Ok();
    }
}