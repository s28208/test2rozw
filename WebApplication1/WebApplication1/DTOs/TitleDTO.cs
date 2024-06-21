using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace WebApplication3.Models;

public class TitleDTO
{
   
    public string Title { get; set; } = string.Empty;
    public DateOnly AquiredAt { get; set; } 

}