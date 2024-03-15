using LingoLabs.Application.Models.Identity;

namespace LingoLabs.Application.Contracts.Identity
{
    public interface IRegistrationServiceStrategy
    {
        public Task<(int status, string message)> Registration(RegistrationModel model);
    }
}
