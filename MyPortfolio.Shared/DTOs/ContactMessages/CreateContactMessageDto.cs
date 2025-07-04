using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Shared.DTOs.ContactMessages
{
    public class CreateContactMessageDto
    {
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name must be under 100 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000, ErrorMessage = "Message must be under 1000 characters")]
        public string Message { get; set; } = string.Empty;
    }
}