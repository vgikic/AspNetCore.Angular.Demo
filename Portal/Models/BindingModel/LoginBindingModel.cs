﻿using System.ComponentModel.DataAnnotations;

namespace Portal.Models.BindingModel
{
    public class LoginBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
