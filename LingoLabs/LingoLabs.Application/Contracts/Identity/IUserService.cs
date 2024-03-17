using LingoLabs.Application.Models.Identity;
using LingoLabs.Domain.Common;

namespace LingoLabs.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<Result<UserInfoModel>> GetCurrentUserInfoAsync(string userId);
        Task<(int status, string message)> ApproveAdmin(string userId);
        Task<Result<List<UserInfoModel>>> GetPendingAdmins();
        Task<Result<string>> DeleteCurrentUser(string userId);
    }
}
