using System.Formats.Asn1;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;
using WebApplication3.Models;

namespace WebApplication1.Controllers;

[Route("api/characters")]
[ApiController]
public class CharacterController : ControllerBase
{
    private readonly IDbService _dbService;

    public CharacterController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{characterId:int}")]
    public async Task<IActionResult> GetCaracter(int characterId)
    {
        var character = await _dbService.GetCharacterData(characterId);
        return Ok(character.Select(e => new CharacterDTO()
        {
            FirstName = e.FirstName,
            LastName = e.LastName,
            CurrentWeight = e.CurrentWeight,
            MaxWeight = e.MaxWeight,
            BackpacksItem = e.Backpacks.Select(p => new BackpackItemDTO()
            {
                ItemName = p.Item.Name,
                ItemWeight = p.Item.Weight,
                Amount = p.Amount
            }).ToList(),
            Titles = e.CharacterTitles.Select(p => new TitleDTO()
            {
                Title = p.Title.Name,
                AquiredAt = p.AcquiredId
            }).ToList()
        }));

    }

    [HttpPost("{characterId:int}/backpacks")]
    public async Task<IActionResult> AddNewCaracter(int characterId, NewBackpackDTO newBackpackDto)
    {
        if (!await (_dbService.DoesChacterExist(characterId)))
        {
            throw new AsnContentException("Not Found Character");

        }

        foreach (var tmp in newBackpackDto.Items)
        {
            if (!await (_dbService.DoesItemExist(tmp.IdItem)))
            {
                throw new AsnContentException("Not Found Item");
            }
            if (!await (_dbService.DoesCharactersWeightFree(characterId, tmp.IdItem)))
            {
                throw new AsnContentException($"Not Free Weight for - {tmp.IdItem} id Item");
            }
        }

        var backpackes = new List<Backpack>();
        foreach (var tmpI in newBackpackDto.Items)
        {
            var amount =await _dbService.GetAmountFromBackpack(tmpI.IdItem, characterId);
            if (amount == 0)
            {
                var newb = new Backpack()
                {
                    CharacterId = characterId,
                    ItemId = tmpI.IdItem,
                    Amount = amount + 1
                };
                await _dbService.AddNewItemCharacterOne(newb);

            }else if (amount > 0)
            {
                await _dbService.UpdateItemAmountForCharacter(characterId, tmpI.IdItem, amount + 1);
            }
           
        }

        return Created();
    }
    
}
