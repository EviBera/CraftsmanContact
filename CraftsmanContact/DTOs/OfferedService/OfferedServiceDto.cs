namespace CraftsmanContact.DTOs;

public class OfferedServiceDto
{
    public int OfferedServiceId { get; set; }
    public string OfferedServiceName { get; set; } = String.Empty;
    public string? OfferedServiceDescription { get; set; }
}