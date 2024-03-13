namespace CraftsmanContact.DTOs.Deal;

public class CreateDealRequestDto
{
    public string CraftsmanId { get; set; } = String.Empty;
    public string ClientId { get; set; } = String.Empty;
    public int OfferedServiceId { get; set; }
}