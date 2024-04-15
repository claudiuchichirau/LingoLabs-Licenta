using LingoLabs.App.Contracts.AuthContracts;
using LingoLabs.App.Contracts.LanguageContracts;
using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LingoLabs.App.Services.LanguageServices
{
    public class ChapterDataService : IChapterDataService
    {
        private const string RequestUri = "api/v1/chapters";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ChapterDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<ApiResponse<ChoicerViewModel>> CreateChapterAsync(ChoicerViewModel createChapterViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<ChoicerViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.PostAsJsonAsync(RequestUri, createChapterViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChoicerViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponse<ChoicerViewModel>> DeleteChapterAsync(Guid chapterId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<ChoicerViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.DeleteAsync($"{RequestUri}/{chapterId}");

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<ChoicerViewModel>
                {
                    IsSuccess = false,
                    Message = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChoicerViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<ChoicerViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<List<ChoicerViewModel>> GetAllChaptersAsync()
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

            var chapters = JsonSerializer.Deserialize<List<ChoicerViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return chapters!;
        }

        public async Task<ChoicerViewModel> GetChapterByIdAsync(Guid chapterId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ChoicerViewModel();
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.GetAsync($"{RequestUri}/{chapterId}");
            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            var chapter = JsonSerializer.Deserialize<ChoicerViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return chapter!;
        }

        public async Task<ApiResponse<ChoicerViewModel>> UpdateChapterAsync(ChoicerViewModel updateChapterViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<ChoicerViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var chapterViewModel = new
            {
                updateChapterViewModel.ChapterName,
                updateChapterViewModel.ChapterPriorityNumber,
                updateChapterViewModel.ChapterDescription,
                updateChapterViewModel.ChapterImageData,
                updateChapterViewModel.ChapterVideoLink
            };

            var result = await httpClient.PutAsJsonAsync(RequestUri, chapterViewModel);
            result.EnsureSuccessStatusCode();

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChoicerViewModel>>();

            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<ChoicerViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }
    }
}
