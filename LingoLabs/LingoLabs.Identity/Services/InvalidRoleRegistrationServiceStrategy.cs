using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Application.Models.Identity;
using LingoLabs.Identity.Models;

namespace LingoLabs.Identity.Services
{
    public class InvalidRoleRegistrationServiceStrategy : IRegistrationServiceStrategy
    {
        public async Task<(int status, string message)> Registration(RegistrationModel model)
        {
            return (UserAuthenticationStatus.REGISTRATION_FAIL, "Invalid Role");
        }
    }
}
