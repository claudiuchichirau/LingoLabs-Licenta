using LingoLabs.Application.Models.Identity;

namespace LingoLabs.Application.Contracts.Identity
{
    public interface ILoginService
    {
        Task<(int, string)> Login(LoginModel model);
        Task<(int, string)> Logout();
    }
}
