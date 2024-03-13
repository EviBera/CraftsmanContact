namespace CraftsmanContact.DTOs.Deal;

public class DealDto
{
    public int DealId { get; set; }
    public string CraftsmanId { get; set; } = String.Empty;
    public string ClientId { get; set; } = String.Empty;
    public int OfferedServiceId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsAcceptedByCraftsman { get; set; }
    public bool IsClosedByCraftsman { get; set; }
    public bool IsClosedByClient { get; set; }
}