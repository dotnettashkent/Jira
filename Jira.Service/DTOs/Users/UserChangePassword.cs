﻿using System.ComponentModel.DataAnnotations;

namespace Jira.Service.DTOs.Users
{
    public class UserChangePassword
    {
        [Required(ErrorMessage = "Value must not be null or empty!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Old password must not be null or empty!")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New password must not be null or empty!")]
        public string NewPassword { get; set; }

        [Compare("NewPasword")]
        public string ComfirmPassword { get; set; }
    }
}
