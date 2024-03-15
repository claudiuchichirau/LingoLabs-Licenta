using Microsoft.AspNetCore.Identity;

namespace LingoLabs.Identity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DefaultLanguage { get; set; }
    }
}
