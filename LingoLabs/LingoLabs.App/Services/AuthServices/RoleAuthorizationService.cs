using System.Security.Claims;

namespace LingoLabs.App.Services.AuthServices
{
    public class RoleAuthorizationService
    {
        public bool IsUserAdmin(ClaimsPrincipal user)
        {
            return user.IsInRole("Admin");
        }
    }
}
