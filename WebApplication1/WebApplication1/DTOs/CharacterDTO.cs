using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public class CharacterDTO
{
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public int CurrentWeight { get; set; } 
    public int MaxWeight { get; set; }

    public ICollection<BackpackItemDTO> BackpacksItem { get; set; } = null!;
    public ICollection<TitleDTO> Titles { get; set; } = null!;

}