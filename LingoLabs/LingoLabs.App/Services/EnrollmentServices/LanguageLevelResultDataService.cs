using LingoLabs.App.Contracts.AuthContracts;
using LingoLabs.App.Contracts.EnrollmentContracts;
using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;
using LingoLabs.App.ViewModel.LanguageModels;
using LingoLabs.App.ViewModel.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LingoLabs.App.Services.EnrollmentServices
{
    public class LanguageLevelResultDataService : ILanguageLevelResultDataService
    {
        private const string RequestUri = "api/v1/languagelevelresults";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public LanguageLevelResultDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<LanguageLevelResultResponse>> CreateLanguageLevelResultAsync(LanguageLevelResultViewModel createLanguageLevelResultViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageLevelResultResponse>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.PostAsJsonAsync(RequestUri, createLanguageLevelResultViewModel);

            if (result.IsSuccessStatusCode)
            {
                var languageLevelResult = await result.Content.ReadFromJsonAsync<LanguageLevelResultResponse>();
                return new ApiResponse<LanguageLevelResultResponse>
                {
                    IsSuccess = true,
                    Data = languageLevelResult
                };
            } else
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LanguageLevelResultResponse>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }
        }

        public async Task<ApiResponse<LanguageLevelResultViewModel>> UpdateLanguageLevelResultAsync(LanguageLevelResultViewModel updateLanguageLevelResultViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageLevelResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var languageLevelResultViewModel = new
            {
                updateLanguageLevelResultViewModel.LanguageLevelResultId,
                updateLanguageLevelResultViewModel.IsCompleted
            };

            var result = await httpClient.PutAsJsonAsync(RequestUri, languageLevelResultViewModel);

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LanguageLevelResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LanguageLevelResultViewModel>>();

            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<LanguageLevelResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }
        public async Task<ApiResponse<LanguageLevelResultViewModel>> DeleteLanguageLevelResultAsync(Guid languageLevelResultId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageLevelResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.DeleteAsync($"{RequestUri}/{languageLevelResultId}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LanguageLevelResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LanguageLevelResultViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<LanguageLevelResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<LanguageLevelResultViewModel> GetLanguageLevelResultByIdAsync(Guid languageLevelResultId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/{languageLevelResultId}");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();

            var enrollment = JsonSerializer.Deserialize<LanguageLevelResultViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return enrollment!;
        }
    }
}
