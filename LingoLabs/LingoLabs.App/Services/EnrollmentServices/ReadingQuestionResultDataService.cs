using LingoLabs.App.Contracts.AuthContracts;
using LingoLabs.App.Contracts.EnrollmentContracts;
using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.EnrollmentModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LingoLabs.App.Services.EnrollmentServices
{
    public class ReadingQuestionResultDataService : IReadingQuestionResultDataService
    {
        private const string RequestUri = "api/v1/readingquestionresults";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ReadingQuestionResultDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }


        public Task<ApiResponse<QuestionResultViewModel>> CreateQuestionResultAsync(QuestionResultViewModel createQuestionResultViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<ReadingQuestionResultViewModel>> CreateReadingQuestionAsync(ReadingQuestionResultViewModel createReadingQuestionViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<ReadingQuestionResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.PostAsJsonAsync(RequestUri, createReadingQuestionViewModel);

            if (result.IsSuccessStatusCode)
            {
                var enrollment = await result.Content.ReadFromJsonAsync<ReadingQuestionResultViewModel>();
                return new ApiResponse<ReadingQuestionResultViewModel>
                {
                    IsSuccess = true,
                    Data = enrollment
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ReadingQuestionResultViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponse<QuestionResultViewModel>> DeleteQuestionResultAsync(Guid questionResultId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<QuestionResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.DeleteAsync($"{RequestUri}/{questionResultId}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<QuestionResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<QuestionResultViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<QuestionResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public Task<QuestionResultViewModel> GetQuestionResultByIdAsync(Guid questionResultId)
        {
            throw new NotImplementedException();
        }

        public async Task<ReadingQuestionResultViewModel> GetReadingQuestionByIdAsync(Guid readingQuestionId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/{readingQuestionId}");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();

            var enrollment = JsonSerializer.Deserialize<ReadingQuestionResultViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return enrollment!;
        }

        public async Task<ApiResponse<QuestionResultViewModel>> UpdateQuestionResultAsync(QuestionResultViewModel updateQuestionResultViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<QuestionResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var languageLevelResultViewModel = new
            {
                updateQuestionResultViewModel.QuestionResultId,
                updateQuestionResultViewModel.IsCorrect
            };

            var result = await httpClient.PutAsJsonAsync(RequestUri, languageLevelResultViewModel);

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<QuestionResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<QuestionResultViewModel>>();

            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<QuestionResultViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }
    }
}
