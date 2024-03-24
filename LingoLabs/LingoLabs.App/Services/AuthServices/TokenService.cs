using Blazored.LocalStorage;
using LingoLabs.App.Contracts.AuthContracts;

namespace LingoLabs.App.Services.AuthServices
{
    public class TokenService : ITokenService
    {
        private const string TOKEN = "token";
        private readonly ILocalStorageService localStorageService;

        public TokenService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task SetTokenAsync(string token)
        {
            await localStorageService.SetItemAsync(TOKEN, token);
        }

        public async Task<string> GetTokenAsync()
        {
            return await localStorageService.GetItemAsync<string>(TOKEN);
        }

        public async Task RemoveTokenAsync()
        {
            await localStorageService.RemoveItemAsync(TOKEN);
        }
    }
}
