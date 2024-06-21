using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models;

public class BackpackItemDTO
{
    
    public string ItemName { get; set; } = string.Empty;
    public int ItemWeight { get; set; } 
    public int Amount { get; set; }
}