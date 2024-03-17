using LingoLabs.Application.Models.Identity;
using LingoLabs.Domain.Common;

namespace LingoLabs.Application.Contracts.Identity
{
    public interface IChangeAdminPendingRoleToAdmin
    {
        Task<Result<UserInfoModel>> ChangeAdminPendingRole(string userId);
    }
}
