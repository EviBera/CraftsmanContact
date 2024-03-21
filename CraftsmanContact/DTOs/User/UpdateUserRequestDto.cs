using System.ComponentModel.DataAnnotations;

namespace CraftsmanContact.DTOs.User;

public class UpdateUserRequestDto
{
    [Required]
    [MaxLength(50, ErrorMessage = "Firstname cannot exceed 50 characters.")]
    public string FirstName { get; set; } = String.Empty;
    [Required]
    [MaxLength(50, ErrorMessage = "Lastname cannot exceed 50 characters.")]
    public string LastName { get; set; } = String.Empty;
    [Required]
    [MaxLength(50, ErrorMessage = "Email address cannot exceed 50 characters.")]
    [RegularExpression( @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$",ErrorMessage = "Please provide valid email address.")]
    public string Email { get; set; } = String.Empty;
    [Required]
    [MaxLength(50, ErrorMessage = "Phone number cannot exceed 50 characters.")]
    public string PhoneNumber { get; set; } = String.Empty;
}