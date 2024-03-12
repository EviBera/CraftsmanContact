namespace CraftsmanContact.DTOs;

public class CreateRequestOfferedServiceDto
{
    public string OfferedServiceName { get; set; } = String.Empty;
    public string? OfferedServiceDescription { get; set; }
}