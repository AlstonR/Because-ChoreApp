using System;
using System.ComponentModel.DataAnnotations;

namespace because.Models
{
    public class SigninModel
    {
        [Required]
        public string Email { get; set; }

        [Required, MaxLength(20)]
        public string Password { get; set; }
    }
}

