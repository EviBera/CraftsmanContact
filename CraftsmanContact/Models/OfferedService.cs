using System.ComponentModel.DataAnnotations;

namespace CraftsmanContact.Models;

public class OfferedService
{
    [Key] 
    public int Id { get; set; }
    [StringLength(50)]
    public string Name { get; set; }
    [StringLength(300)]
    public string? Description { get; set; }
    
}