using LingoLabs.App.Contracts;
using LingoLabs.App.Contracts.AuthContracts;
using LingoLabs.App.ViewModel.AuthenticationModels;
using System.Net.Http.Json;

namespace LingoLabs.App.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string RequestUri = "api/v1/authentication";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public AuthenticationService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task Login(LoginViewModel loginRequest)
        {
            var response = await httpClient.PostAsJsonAsync($"{RequestUri}/login", loginRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();
            await tokenService.SetTokenAsync(token);
        }

        public async Task Logout()
        {
            await tokenService.RemoveTokenAsync();
            var result = await httpClient.PostAsync($"{RequestUri}/logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterViewModel registerRequest)
        {
            var result = await httpClient.PostAsJsonAsync($"{RequestUri}/register", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
            result.EnsureSuccessStatusCode();
        }
    }
}
