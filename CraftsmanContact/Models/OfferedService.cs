namespace CraftsmanContact.Models;

public class OfferedService
{
    private static uint _id;
    public uint Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; set; }

    
    public OfferedService(string name, string description)
    {
        Id = _id++;
        Name = name;
        Description = description;
    }
}