using System.Formats.Asn1;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication1.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Character>> GetCharacterData(int idCharacter)
    {
        if (!await (DoesChacterExist(idCharacter)))
        {
            throw new AsnContentException("Not Found Character");
            
        }
        return await _context.Character

            .Include(e => e.CharacterTitles)
            .ThenInclude(e => e.Title)
            .Include(e => e.Backpacks)
            .ThenInclude(e => e.Item)
            .Where(e => e.IdCharacter == idCharacter)
            .ToListAsync();
        
    }

    public async Task<int> GetAmountFromBackpack( int idCharacter, int idItem)
    {
        var backpackEntry = await _context.Backpack
            .FirstOrDefaultAsync(b => b.ItemId == idItem && b.CharacterId == idCharacter);

        return backpackEntry?.Amount ?? 0;    
    }

    
    public async Task AddNewItemCharacter(List<Backpack> backpacks)
    {
        await _context.AddRangeAsync(backpacks);
        await _context.SaveChangesAsync();
    }
    public async Task AddNewItemCharacterOne(Backpack backpacks)
    {
        await _context.AddAsync(backpacks);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateItemAmountForCharacter(int idCharacter, int idItem, int newAmount)
    {
        var backpackEntry = await _context.Backpack
            .FirstOrDefaultAsync(b => b.ItemId == idItem && b.CharacterId == idCharacter);

        if (backpackEntry != null)
        {
            backpackEntry.Amount = newAmount;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> DoesChacterExist(int CharacterId)
    {
        return await _context.Character.AnyAsync(e => e.IdCharacter == CharacterId);
    }
    public async Task<bool> DoesItemExist(int ItemId)
    {
        return await _context.Item.AnyAsync(e => e.IdItem == ItemId);
    }
    public async Task<bool> DoesCharactersWeightFree(int characterId, int ItemId)
    {
        var character = await _context.Character
            .FirstOrDefaultAsync(c => c.IdCharacter == characterId);
        var item = await _context.Item
            .FirstOrDefaultAsync(c => c.IdItem == ItemId);
        int maxWeigt = character.MaxWeight;
        int currentWeigt = character.CurrentWeight;
        int itemWeight = item.Weight;

        return maxWeigt - (currentWeigt + itemWeight) >= 0;
    }


}