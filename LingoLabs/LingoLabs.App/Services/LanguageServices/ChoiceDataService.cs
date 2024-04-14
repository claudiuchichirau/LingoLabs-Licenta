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
                throw new ApplicationException("Authentication token is null.");
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
            try
            {
                var token = await tokenService.GetTokenAsync();
                if (token == null)
                {
                    throw new ApplicationException("Authentication token is null.");
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

                throw new ApplicationException("Failed to deserialize the response JSON.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<ApiResponse<ChoiceViewModel>> DeleteChoiceAsync(Guid choiceId)
        {
            var result = await httpClient.DeleteAsync($"{RequestUri}/{choiceId}");

            result.EnsureSuccessStatusCode();

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                throw new ApplicationException(content);
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
            var result = await httpClient.GetAsync($"{RequestUri}/{choiceId}");

            result.EnsureSuccessStatusCode();

            //var response = await result.Content.ReadFromJsonAsync<ApiResponse<ChoiceViewModel>>();
            //response!.IsSuccess = result.IsSuccessStatusCode;
            //return response!;

            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var choiceViewModel = JsonSerializer.Deserialize<ChoiceViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new ApiResponse<ChoiceViewModel> { Data = choiceViewModel, IsSuccess = true }.Data!;
        }
    }
}
