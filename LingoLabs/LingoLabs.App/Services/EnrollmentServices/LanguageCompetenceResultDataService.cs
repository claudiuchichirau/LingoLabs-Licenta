using LingoLabs.App.Contracts.AuthContracts;
using LingoLabs.App.Contracts.EnrollmentContracts;
using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;
using LingoLabs.App.ViewModel.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LingoLabs.App.Services.EnrollmentServices
{
    public class LanguageCompetenceResultDataService : ILanguageCompetenceResultDataService
    {
        private const string RequestUri = "api/v1/languagecompetenceresults";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public LanguageCompetenceResultDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<LanguageCompetenceResultResponse>> CreateLanguageCompetenceResultAsync(LanguageCompetenceResultViewModel createLanguageCompetenceResultViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageCompetenceResultResponse>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.PostAsJsonAsync(RequestUri, createLanguageCompetenceResultViewModel);

            if (result.IsSuccessStatusCode)
            {
                var enrollment = await result.Content.ReadFromJsonAsync<LanguageCompetenceResultResponse>();
                return new ApiResponse<LanguageCompetenceResultResponse>
                {
                    IsSuccess = true,
                    Data = enrollment
                };
            }
            else
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LanguageCompetenceResultResponse>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }
        }

        public async Task<ApiResponse<LanguageCompetenceResultViewModel>> UpdateLanguageCompetenceResultAsync(LanguageCompetenceResultViewModel updateLanguageCompetenceResultViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageCompetenceResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var languageLevelResultViewModel = new
            {
                updateLanguageCompetenceResultViewModel.LanguageCompetenceResultId,
                updateLanguageCompetenceResultViewModel.IsCompleted
            };

            var result = await httpClient.PutAsJsonAsync(RequestUri, languageLevelResultViewModel);

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LanguageCompetenceResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LanguageCompetenceResultViewModel>>();

            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<LanguageCompetenceResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }
        public async Task<ApiResponse<LanguageCompetenceResultViewModel>> DeleteLanguageCompetenceResultAsync(Guid languageCompetenceResultId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LanguageCompetenceResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.DeleteAsync($"{RequestUri}/{languageCompetenceResultId}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LanguageCompetenceResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LanguageCompetenceResultViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<LanguageCompetenceResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<LanguageCompetenceResultViewModel> GetLanguageCompetenceResultByIdAsync(Guid languageCompetenceResultId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/{languageCompetenceResultId}");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();

            var enrollment = JsonSerializer.Deserialize<LanguageCompetenceResultViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return enrollment!;
        }
    }
}
