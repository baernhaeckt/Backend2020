using System.ComponentModel.DataAnnotations;

namespace Backend.Core.Features.UserManagement.Requests
{
    public class UserLoginRequest
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;
    }
}