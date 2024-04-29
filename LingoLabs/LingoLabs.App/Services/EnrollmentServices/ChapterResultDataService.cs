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
    public class ChapterResultDataService : IChapterResultDataService
    {
        private const string RequestUri = "api/v1/chapterresults";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ChapterResultDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<ChapterResultResponse>> CreateChapterResultAsync(ChapterResultViewModel createChapterResultViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<ChapterResultResponse>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.PostAsJsonAsync(RequestUri, createChapterResultViewModel);

            if (result.IsSuccessStatusCode)
            {
                var enrollment = await result.Content.ReadFromJsonAsync<ChapterResultResponse>();
                return new ApiResponse<ChapterResultResponse>
                {
                    IsSuccess = true,
                    Data = enrollment
                };
            } else
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<ChapterResultResponse>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }
        }

        public async Task<ApiResponse<ChapterResultViewModel>> UpdateChapterResultAsync(ChapterResultViewModel updateChapterResultViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<ChapterResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var chapterResultViewModel = new
            {
                updateChapterResultViewModel.ChapterResultId,
                updateChapterResultViewModel.IsCompleted
            };

            var result = await httpClient.PutAsJsonAsync(RequestUri, chapterResultViewModel);

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<ChapterResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChapterResultViewModel>>();

            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<ChapterResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<ApiResponse<ChapterResultViewModel>> DeleteChapterResultAsync(Guid chapterResultId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<ChapterResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.DeleteAsync($"{RequestUri}/{chapterResultId}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<ChapterResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChapterResultViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<ChapterResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<ChapterResultViewModel> GetChapterResultByIdAsync(Guid chapterResultId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/{chapterResultId}");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();

            var enrollment = JsonSerializer.Deserialize<ChapterResultViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return enrollment!;
        }
    }
}
