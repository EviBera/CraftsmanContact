namespace CraftsmanContact.DTOs.OfferedService;

public class UpdateRequestOfferedServiceDto
{
    public string OfferedServiceName { get; set; } = String.Empty;
    public string? OfferedServiceDescription { get; set; }
}