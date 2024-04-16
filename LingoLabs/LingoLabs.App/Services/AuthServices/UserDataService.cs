using LingoLabs.App.Contracts.AuthContracts;
using LingoLabs.App.Services.Responses;
using LingoLabs.App.ViewModel.AuthenticationModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LingoLabs.App.Services.AuthServices
{
    public class UserDataService : IUserDataService
    {
        private const string RequestUri = "api/v1/userInfo";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private readonly RoleAuthorizationService roleAuthorizationService;

        public UserDataService(HttpClient httpClient, ITokenService tokenService, RoleAuthorizationService roleAuthorizationService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
            this.roleAuthorizationService = roleAuthorizationService;
        }

        public async Task<ApiResponse<UserDto>> ApproveAdminAsync(Guid userId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.PutAsync($"{RequestUri}/approveadmin/{userId}", null);

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = content
                };
            }

            return new ApiResponse<UserDto> { IsSuccess = true };
        }

        public async Task<ApiResponse<UserDto>> ChangeUserPasswordAsync(ChangePasswordViewModel changePassword)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(changePassword), System.Text.Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync($"{RequestUri}/change-password", content);
            if (!result.IsSuccessStatusCode)
            {
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = await result.Content.ReadAsStringAsync()
                };
            }

            return new ApiResponse<UserDto> { IsSuccess = true };
        }

        public async Task<ApiResponse<UserDto>> DeleteCurrentUserAsync()
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.DeleteAsync($"{RequestUri}/deletecurrentuser");

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = content
                };
            }

            return new ApiResponse<UserDto> { IsSuccess = true };
        }

        public async Task<List<UserDto>> GetPendingAdminsAsync()
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                throw new ApplicationException("Authentication token is null.");
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.GetAsync($"{RequestUri}/pendingadmins", HttpCompletionOption.ResponseHeadersRead);

            var content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<UserDto>();
                }
            }

            var userDtos = JsonSerializer.Deserialize<List<UserDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return userDtos!;
        }

        public async Task<ApiResponse<UserDto>> GetUserInfoAsync()
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

            var userDto = JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new ApiResponse<UserDto> { Data = userDto, IsSuccess = true };
        }

        public async Task<ApiResponse<UserDto>> RejectAdminAsync(Guid userId)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await httpClient.PutAsync($"{RequestUri}/rejectadmin/{userId}", null);
            result.EnsureSuccessStatusCode();

            if (!result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = content
                };
            }

            return new ApiResponse<UserDto> { IsSuccess = true };
        }

        public async Task<ApiResponse<UserDto>> UpdateUserInfoAsync(UserDto user)
        {
            var token = await tokenService.GetTokenAsync();
            if (token == null)
            {
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = "Authentication token is null."
                };
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync($"{RequestUri}/update-user-info", content);

            if (!result.IsSuccessStatusCode)
            {
                var responseContent = await result.Content.ReadAsStringAsync();
                return new ApiResponse<UserDto>
                {
                    IsSuccess = false,
                    Message = responseContent
                };
            }

            return new ApiResponse<UserDto> { IsSuccess = true };
        }
    }
}
