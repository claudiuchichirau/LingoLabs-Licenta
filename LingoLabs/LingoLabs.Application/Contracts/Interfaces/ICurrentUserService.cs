using System.Security.Claims;

namespace LingoLabs.Application.Contracts.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        ClaimsPrincipal GetCurrentClaimsPrincipal();
        string GetCurrentUserId();
        bool IsUserAdmin();
    }
}
