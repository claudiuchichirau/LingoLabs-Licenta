using System.ComponentModel.DataAnnotations;

namespace LingoLabs.Application.Models.Identity
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Current Password is required")]
        public string? CurrentPassword { get; set; } 

        [Required(ErrorMessage = "New Password is required")]
        public string? NewPassword { get; set; }
    }
}
