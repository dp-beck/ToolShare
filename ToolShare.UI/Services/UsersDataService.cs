using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using ToolShare.UI.Dtos;


namespace ToolShare.UI.Services
{
    public class UsersDataService : IUsersDataService
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions jsonSerializerOptions =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = ReferenceHandler.Preserve
            };

        public UsersDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AppUserDto> GetCurrentUser()
        {
            var response = await _httpClient.GetAsync("api/users/current-user");
            response.EnsureSuccessStatusCode();
            var JsonResponse = await response.Content.ReadAsStringAsync();
            var userInfo = JsonSerializer.Deserialize<AppUserDto>(JsonResponse, jsonSerializerOptions);
            return userInfo;
        }

        public async Task<AppUserDto> FindUserByUsername(string username)
        {
            var response = await _httpClient.GetAsync($"api/users/{username}");
            response.EnsureSuccessStatusCode();
            var JsonResponse = await response.Content.ReadAsStringAsync();
            var appUser = JsonSerializer.Deserialize<AppUserDto>(JsonResponse, jsonSerializerOptions);
            return appUser;
        }

        public async Task<IQueryable<AppUserDto>> GetNoPodUsers()
        {
            var response = await _httpClient.GetAsync("api/users/users-without-pods");
            response.EnsureSuccessStatusCode();
            var JsonResponse = await response.Content.ReadAsStringAsync();
            var NoPodUsers = JsonSerializer.Deserialize<IEnumerable<AppUserDto>>(JsonResponse, jsonSerializerOptions)
                .AsQueryable();

            return NoPodUsers;
        }

        public async Task<FormResult> UpdateCurrentUser(UserInfoUpdateDto userInfoUpdateDto)
        {
            string[] defaultDetail = ["An unknown error prevented the user from being updated."];

            try
            {
                var result = await _httpClient.PutAsJsonAsync("api/users/update", userInfoUpdateDto);
                if (result.IsSuccessStatusCode)
                {
                    return new FormResult { Succeeded = true };
                }

                var details = await result.Content.ReadAsStringAsync();
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = [details]
                };
            }
            catch (Exception e)
            {
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };
            }
        }

        public async Task<FormResult> UpdatePassword(ChangePasswordDto changePasswordDto)
        {
            string[] defaultDetail = ["An unknown error prevented the user from changing the password."];

            try
            {
                var result = await _httpClient.PutAsJsonAsync("api/users/change-password", changePasswordDto);
                if (result.IsSuccessStatusCode)
                {
                    return new FormResult { Succeeded = true };
                }

                var details = await result.Content.ReadAsStringAsync();

                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = [details]
                };
            }
            catch (Exception e)
            {
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };
            }
        }

        public async Task<FormResult> DeleteCurrentUser()
        {
            string[] defaultDetail = ["An unknown error prevented the user from changing the password."];

            try
            {
                var result = await _httpClient.DeleteAsync("api/users/delete");
                if (result.IsSuccessStatusCode)
                {
                    return new FormResult { Succeeded = true };
                }

                var details = await result.Content.ReadAsStringAsync();

                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = [details]
                };
            }
            catch (Exception e)
            {
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };
            }
        }
    }
}