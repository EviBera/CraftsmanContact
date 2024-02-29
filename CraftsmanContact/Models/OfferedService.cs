using System.ComponentModel.DataAnnotations;

namespace CraftsmanContact.Models;

public class OfferedService
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }


    public OfferedService()
    {
        
    }
    /*
    public OfferedService(string name, string description)
    {
        Name = name;
        Description = description;
    }
*/
}