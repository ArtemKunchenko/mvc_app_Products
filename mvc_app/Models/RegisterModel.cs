﻿using System.ComponentModel.DataAnnotations;

namespace mvc_app.Models
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]      
        public string Password { get; set; }
    }
}
