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
    public class LessonResultDataService : ILessonResultDataService
    {
        private const string RequestUri = "api/v1/lessonresults";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public LessonResultDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<LessonResultResponse>> CreateLessonResultAsync(LessonResultViewModel createLessonResultViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LessonResultResponse>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.PostAsJsonAsync(RequestUri, createLessonResultViewModel);

            if (result.IsSuccessStatusCode)
            {
                var enrollment = await result.Content.ReadFromJsonAsync<LessonResultResponse>();
                return new ApiResponse<LessonResultResponse>
                {
                    IsSuccess = true,
                    Data = enrollment
                };
            }
            else
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LessonResultResponse>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }
        }

        public async Task<ApiResponse<LessonResultViewModel>> UpdateLessonResultAsync(LessonResultViewModel updateLessonResultViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LessonResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var lessonResultViewModel = new
            {
                updateLessonResultViewModel.LessonResultId,
                updateLessonResultViewModel.IsCompleted
            };

            var result = await httpClient.PutAsJsonAsync(RequestUri, lessonResultViewModel);

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LessonResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LessonResultViewModel>>();

            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<LessonResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<ApiResponse<LessonResultViewModel>> DeleteLessonResultAsync(Guid lessonResultId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<LessonResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.DeleteAsync($"{RequestUri}/{lessonResultId}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<LessonResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<LessonResultViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<LessonResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<LessonResultViewModel> GetLessonResultByIdAsync(Guid lessonResultId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/{lessonResultId}");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();

            var enrollment = JsonSerializer.Deserialize<LessonResultViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return enrollment!;
        }
    }
}
