using LingoLabs.App.Contracts.AuthContracts;
using LingoLabs.App.Contracts.LanguageContracts;
using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.LanguageModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LingoLabs.App.Services.LanguageServices
{
    public class ChoiceDataService : IChoiceDataService
    {
        private const string RequestUri = "api/v1/choices";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ChoiceDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<ChoiceViewModel>> CreateChoiceAsync(ChoiceViewModel createChoiceViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<ChoiceViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.PostAsJsonAsync(RequestUri, createChoiceViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChoiceViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponse<ChoiceViewModel>> UpdateChoiceAsync(ChoiceViewModel updateChoiceViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<ChoiceViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var choiceViewModel = new
            {
                ChoiceId = updateChoiceViewModel.ChoiceId,
                ChoiceContent = updateChoiceViewModel.ChoiceContent,
                IsCorrect = updateChoiceViewModel.IsCorrect
                    
            };

            var result = await httpClient.PutAsJsonAsync(RequestUri, choiceViewModel);
            result.EnsureSuccessStatusCode();

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChoiceViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }

            return new ApiResponse<ChoiceViewModel>
            {
                IsSuccess = false,
                ValidationErrors = response.ValidationErrors,
            };

        }

        public async Task<ApiResponse<ChoiceViewModel>> DeleteChoiceAsync(Guid choiceId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<ChoiceViewModel>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            var result = await httpClient.DeleteAsync($"{RequestUri}/{choiceId}");

            result.EnsureSuccessStatusCode();

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<ChoiceViewModel>
                {
                    IsSuccess = false,
                    Message = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChoiceViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<ChoiceViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors,
                };
            }
        }

        public async Task<ChoiceViewModel> GetChoiceByIdAsync(Guid choiceId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new Exception("Authentication token is null.");
            }

            var result = await httpClient.GetAsync($"{RequestUri}/{choiceId}");

            result.EnsureSuccessStatusCode();

            //var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChoiceViewModel>>();
            //response!.IsSuccess = result.IsSuccessStatusCode;
            //return response!;

            var content = await result.Content.ReadAsStringAsync();

            var choiceViewModel = JsonSerializer.Deserialize<ChoiceViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new ApiResponse<ChoiceViewModel> { Data = choiceViewModel, IsSuccess = true }.Data!;
        }
    }
}
