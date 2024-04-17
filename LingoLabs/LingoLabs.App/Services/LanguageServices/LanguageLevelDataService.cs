using LingoLabs.App.Contracts.AuthContracts;
using LingoLabs.App.Contracts.LanguageContracts;
using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LingoLabs.App.Services.LanguageServices
{
    public class LanguageLevelDataService : ILanguageLevelDataService
    {
        private const string RequestUri = "api/v1/languagelevels";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public LanguageLevelDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<LanguageLevelViewModel>> CreateLanguageLevelAsync(LanguageLevelViewModel createLanguageLevelViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageLevelViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.PostAsJsonAsync(RequestUri, createLanguageLevelViewModel);

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LanguageLevelViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LanguageLevelViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponse<LanguageLevelViewModel>> DeleteLanguageLevelAsync(Guid languageLevelId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageLevelViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.DeleteAsync($"{RequestUri}/{languageLevelId}");

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LanguageLevelViewModel>
                {
                    IsSuccess = false,
                    Message = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LanguageLevelViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<LanguageLevelViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<List<LanguageLevelViewModel>> GetAllLanguageLevelsAsync()
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

            var languageLevels = JsonSerializer.Deserialize<List<LanguageLevelViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return languageLevels!;
        }

        public async Task<LanguageLevelViewModel> GetLanguageLevelByIdAsync(Guid languageLevelId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/{languageLevelId}");

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            var languageLevel = JsonSerializer.Deserialize<LanguageLevelViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return languageLevel!;
        }

        public async Task<ApiResponse<LanguageLevelViewModel>> UpdateLanguageLevelAsync(LanguageLevelViewModel updateLanguageLevelViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageLevelViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var languageLevelViewModel = new
            {
                updateLanguageLevelViewModel.LanguageLevelId,
                updateLanguageLevelViewModel.LanguageLevelName,
                updateLanguageLevelViewModel.LanguageLevelAlias,
                updateLanguageLevelViewModel.LanguageLevelDescription,
                updateLanguageLevelViewModel.LanguageLevelVideoLink,
                updateLanguageLevelViewModel.LanguageLevelPriorityNumber,
            };

            var result = await httpClient.PutAsJsonAsync(RequestUri, languageLevelViewModel);

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LanguageLevelViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LanguageLevelViewModel>>();

            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<LanguageLevelViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }
    }
}
