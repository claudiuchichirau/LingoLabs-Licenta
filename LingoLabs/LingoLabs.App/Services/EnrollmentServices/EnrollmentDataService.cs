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
    public class EnrollmentDataService : IEnrollmentDataService
    {
        private const string RequestUri = "api/v1/enrollments";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public EnrollmentDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<ApiResponse<EnrollmentViewModel>> CreateEnrollmentAsync(EnrollmentViewModel createEnrollmentViewModel)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<EnrollmentViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.PostAsJsonAsync(RequestUri, createEnrollmentViewModel);

            if (result.IsSuccessStatusCode)
            {
                var enrollment = await result.Content.ReadFromJsonAsync<EnrollmentResponse>();
                return new ApiResponse<EnrollmentViewModel>
                {
                    IsSuccess = true,
                    Data = enrollment.Enrollment
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<EnrollmentViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task<ApiResponse<EnrollmentViewModel>> DeleteEnrollmentAsync(Guid enrollmentId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<EnrollmentViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.DeleteAsync($"{RequestUri}/{enrollmentId}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<EnrollmentViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = content
                };
            }

            var response = await result.Content.ReadFromJsonAsync<ApiResponse<EnrollmentViewModel>>();
            if (response != null)
            {
                response.IsSuccess = result.IsSuccessStatusCode;
                return response;
            }
            else
            {
                return new ApiResponse<EnrollmentViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = response.ValidationErrors
                };
            }
        }

        public async Task<List<EnrollmentViewModel>> GetAllEnrollmentsAsync()
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

            var enrollments = JsonSerializer.Deserialize<List<EnrollmentViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return enrollments!;
        }

        public async Task<UserEnrollmentsResponse> GetAllEnrollmentsByUserIdAsync()
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/userEnrollments", HttpCompletionOption.ResponseHeadersRead);

            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var enrollments = JsonSerializer.Deserialize<UserEnrollmentsResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return enrollments!;
        }

        public async Task<EnrollmentViewModel> GetEnrollmentByIdAsync(Guid enrollmentId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await httpClient.GetAsync($"{RequestUri}/{enrollmentId}");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();

            var enrollment = JsonSerializer.Deserialize<EnrollmentViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return enrollment!;

        }
    }
}
