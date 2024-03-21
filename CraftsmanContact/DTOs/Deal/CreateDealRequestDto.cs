using System.ComponentModel.DataAnnotations;

namespace CraftsmanContact.DTOs.Deal;

public class CreateDealRequestDto
{
    [Required]
    public string CraftsmanId { get; set; } = String.Empty;
    [Required]
    public string ClientId { get; set; } = String.Empty;
    [Required]
    public int OfferedServiceId { get; set; }
}