using LingoLabs.App.Contracts.AuthContracts;
using LingoLabs.App.Contracts.LanguageContracts;
using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LingoLabs.App.Services.LanguageServices
{
    public class LanguageCompetenceDataService : ILanguageCompetenceDataService
    {
        private const string RequestUri = "api/v1/languagecompetences";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public LanguageCompetenceDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<LanguageCompetenceViewModel>> CreateLanguageCompetenceAsync(LanguageCompetenceViewModel createLanguageCompetenceViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageCompetenceViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.PostAsJsonAsync(RequestUri, createLanguageCompetenceViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LanguageCompetenceViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponse<LanguageCompetenceViewModel>> DeleteLanguageCompetenceAsync(Guid languageCompetenceId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageCompetenceViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.DeleteAsync($"{RequestUri}/{languageCompetenceId}");

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LanguageCompetenceViewModel>
                {
                    IsSuccess = false,
                    Message = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LanguageCompetenceViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<LanguageCompetenceViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<List<LanguageCompetenceViewModel>> GetAllLanguageCompetencesAsync()
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var languageCompetences = JsonSerializer.Deserialize<List<LanguageCompetenceViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return languageCompetences!;
        }

        public async Task<LanguageCompetenceViewModel> GetLanguageCompetenceByIdAsync(Guid languageCompetenceId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/{languageCompetenceId}");

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            var languageCompetence = JsonSerializer.Deserialize<LanguageCompetenceViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return languageCompetence!;
        }

        public async Task<ApiResponse<LanguageCompetenceViewModel>> UpdateLanguageCompetenceAsync(LanguageCompetenceViewModel updateLanguageCompetenceViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageCompetenceViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var languageCompetenceViewModel = new
            {
                updateLanguageCompetenceViewModel.LanguageCompetenceName,
                updateLanguageCompetenceViewModel.LanguageCompetenceDescription,
                updateLanguageCompetenceViewModel.LanguageCompetenceVideoLink,
                updateLanguageCompetenceViewModel.LanguageCompetencePriorityNumber
            };

            var result = await httpClient.PutAsJsonAsync($"{RequestUri}/{updateLanguageCompetenceViewModel.LanguageCompetenceId}", languageCompetenceViewModel);
            result.EnsureSuccessStatusCode();

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LanguageCompetenceViewModel>>();

            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<LanguageCompetenceViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }
    }
}
