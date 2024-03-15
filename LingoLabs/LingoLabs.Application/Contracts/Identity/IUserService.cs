using LingoLabs.Application.Models.Identity;
using LingoLabs.Domain.Common;

namespace LingoLabs.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<Result<UserInfoModel>> GetCurrentUserInfoAsync(string userId);
    }
}
