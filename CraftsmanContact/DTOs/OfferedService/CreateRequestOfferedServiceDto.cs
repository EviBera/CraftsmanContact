using System.ComponentModel.DataAnnotations;

namespace CraftsmanContact.DTOs.OfferedService;

public class CreateRequestOfferedServiceDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Service name must be at least 3 characters.")]
    [MaxLength(50, ErrorMessage = "Service name cannot exceed 50 characters.")]
    public string OfferedServiceName { get; set; } = String.Empty;
    [MaxLength(300, ErrorMessage = "Service description cannot exceed 300 characters.")]
    public string? OfferedServiceDescription { get; set; }
}