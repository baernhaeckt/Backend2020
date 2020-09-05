﻿using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class RegisterUserRequest
    {
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;
    }
}