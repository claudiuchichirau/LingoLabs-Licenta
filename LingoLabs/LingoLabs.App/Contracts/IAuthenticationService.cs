using LingoLabs.App.ViewModel.AuthenticationModels;

namespace LingoLabs.App.Contracts
{
    public interface IAuthenticationService
    {
        Task Login(LoginViewModel loginRequest);
        Task Register(RegisterViewModel registerRequest);
        Task Logout();
    }
}
