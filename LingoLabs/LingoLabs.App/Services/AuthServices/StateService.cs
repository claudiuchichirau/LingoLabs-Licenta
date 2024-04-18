using Blazored.LocalStorage;
using LingoLabs.App.Contracts.AuthContracts;

namespace LingoLabs.App.Services.AuthServices
{
    public class StateService : IStateService
    {
        private IServiceProvider _serviceProvider;

        public StateService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Guid> GetId()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var localStorageService = serviceScope.ServiceProvider.GetRequiredService<ILocalStorageService>();
            var ids = await localStorageService.GetItemAsync<Stack<Guid>>("ids");
            return ids.Peek(); // returnează elementul de la vârful stivei fără a-l elimina
        }

        public async Task RemoveId()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var localStorageService = serviceScope.ServiceProvider.GetRequiredService<ILocalStorageService>();
            var ids = await localStorageService.GetItemAsync<Stack<Guid>>("ids");
            ids.Pop(); // elimină elementul de la vârful stivei
            await localStorageService.SetItemAsync("ids", ids);
        }

        public async Task SetId(Guid id)
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var localStorageService = serviceScope.ServiceProvider.GetRequiredService<ILocalStorageService>();
            var ids = await localStorageService.GetItemAsync<Stack<Guid>>("ids");
            if (ids == null)
            {
                ids = new Stack<Guid>();
            }
            ids.Push(id); // adaugă un nou element la vârful stivei
            await localStorageService.SetItemAsync("ids", ids);
        }

        public async Task ClearIds()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var localStorageService = serviceScope.ServiceProvider.GetRequiredService<ILocalStorageService>();
            var ids = await localStorageService.GetItemAsync<Stack<Guid>>("ids");
            ids.Clear(); // elimină toate elementele din stivă
            await localStorageService.SetItemAsync("ids", ids);
        }
    }
}


//using LingoLabs.App.Contracts.AuthContracts;

//namespace LingoLabs.App.Services.AuthServices
//{
//    public class StateService : IStateService
//    {
//        private Guid Id { get; set; }


//        public Task<Guid> GetId()
//        {
//            return Task.FromResult(Id);
//        }

//        public Task RemoveId()
//        {
//            Id = Guid.Empty;
//            return Task.CompletedTask;
//        }

//        public Task SetId(Guid id)
//        {
//            Id = id;
//            return Task.CompletedTask;
//        }
//    }
//}
