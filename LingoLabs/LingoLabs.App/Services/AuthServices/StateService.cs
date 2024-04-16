using Blazored.LocalStorage;
using LingoLabs.App.Contracts.AuthContracts;

namespace LingoLabs.App.Services.AuthServices
{
    public class StateService : IStateService
    {
        private Guid Id { get; set; }


        public Task<Guid> GetId()
        {
            return Task.FromResult(Id);
        }

        public Task RemoveId()
        {
            Id = Guid.Empty;
            return Task.CompletedTask;
        }

        public Task SetId(Guid id)
        {
            Id = id;
            return Task.CompletedTask;
        }
    }
}
