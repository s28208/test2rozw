using WebApplication3.Models;

namespace WebApplication1.Services;

public interface IDbService
{
    public Task<ICollection<Character>> GetCharacterData(int idCharacter);
    public Task<bool> DoesChacterExist(int CharacterId);
    public Task AddNewItemCharacter(List<Backpack> backpacks);
    public Task<bool> DoesItemExist(int ItemId);
    public Task<bool> DoesCharactersWeightFree(int characterId, int ItemId);
    public Task<int> GetAmountFromBackpack(int idCharacter, int idItem);
    public Task AddNewItemCharacterOne(Backpack backpacks);
    public Task UpdateItemAmountForCharacter(int idCharacter, int idItem, int newAmount);






}