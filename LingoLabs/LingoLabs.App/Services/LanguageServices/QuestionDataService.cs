﻿using LingoLabs.App.Contracts.AuthContracts;
using LingoLabs.App.Contracts.LanguageContracts;
using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;
using LingoLabs.App.ViewModel.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LingoLabs.App.Services.LanguageServices
{
    public class QuestionDataService : IQuestionDataService
    {
        private const string RequestUri = "api/v1/questions";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public QuestionDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<QuestionViewModel>> CreateQuestionAsync(QuestionViewModel createQuestionViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<QuestionViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.PostAsJsonAsync(RequestUri, createQuestionViewModel);

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<QuestionViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<QuestionViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponse<QuestionViewModel>> UpdateQuestionAsync(QuestionViewModel updateQuestionViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<QuestionViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var questionViewModel = new
            {
                updateQuestionViewModel.QuestionId,
                updateQuestionViewModel.QuestionRequirement,
                updateQuestionViewModel.QuestionType,
                updateQuestionViewModel.LessonId,
                updateQuestionViewModel.QuestionPriorityNumber,
                updateQuestionViewModel.QuestionImageData,
                updateQuestionViewModel.QuestionVideoLink,
                updateQuestionViewModel.LanguageId
            };

            var result = await httpClient.PutAsJsonAsync(RequestUri, questionViewModel);

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<QuestionViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<QuestionViewModel>>();

            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<QuestionViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<ApiResponse<QuestionViewModel>> DeleteQuestionAsync(Guid questionId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<QuestionViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            var result = await httpClient.DeleteAsync($"{RequestUri}/{questionId}");    

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<QuestionViewModel>
                {
                    IsSuccess = false,
                    Message = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<QuestionViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<QuestionViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<List<QuestionViewModel>> GetAllQuestionsAsync()
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

            var response = JsonSerializer.Deserialize<QuestionsResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response.Questions!;
        }

        public async Task<List<QuestionViewModel>> GetAllQuestionsByLanguageCompetenceId(Guid languageCompetenceId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/language-competence/{languageCompetenceId}");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var response = JsonSerializer.Deserialize<QuestionsResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response.Questions!;
        }

        public async Task<List<QuestionViewModel>> GetAllQuestionsByLanguageCompetenceIdAndLevelId(Guid languageCompetenceId, Guid languageLevelId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/language-competence/{languageCompetenceId}/language-level/{languageLevelId}");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var response = JsonSerializer.Deserialize<QuestionsResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response.Questions!;
        }

        public async Task<List<QuestionViewModel>> GetAllQuestionsByLanguageLevelId(Guid languageLevelId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/language-level/{languageLevelId}");

            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var response = JsonSerializer.Deserialize<QuestionsResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response.Questions!;
        }

        public async Task<QuestionViewModel> GetQuestionByIdAsync(Guid questionId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/{questionId}");

            result.EnsureSuccessStatusCode();

            var content = await result.Content.ReadAsStringAsync();

            var questionViewModel = JsonSerializer.Deserialize<QuestionViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return questionViewModel!;
        }
    }
}
