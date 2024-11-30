using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;
using ToolShare.UI.Identity.Models;


namespace ToolShare.UI.Services
{
    public class UsersDataService : IUsersDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
         private readonly JsonSerializerOptions jsonSerializerOptions =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = ReferenceHandler.Preserve
            };

        public UsersDataService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<AppUserDTO> GetCurrentUser()
        {
            var response = await _httpClient.GetAsync("api/users/current-user");
            response.EnsureSuccessStatusCode();
            var JsonResponse = await response.Content.ReadAsStringAsync();
            var userInfo = JsonSerializer.Deserialize<AppUserDTO>(JsonResponse, jsonSerializerOptions);
            return userInfo;
        }

        public async Task<AppUserDTO> FindUserByUsername(string username)
        {
            var response = await _httpClient.GetAsync($"api/users/{username}");
            response.EnsureSuccessStatusCode();
            var JsonResponse = await response.Content.ReadAsStringAsync();
            var appUser = JsonSerializer.Deserialize<AppUserDTO>(JsonResponse, jsonSerializerOptions);
            var userInfo = _mapper.Map<AppUserDTO>(appUser);
            return userInfo;
        }

        public async Task<IQueryable<AppUserDTO>> GetNoPodUsers()
        {
            var response = await _httpClient.GetAsync("api/users/users-without-pods");
            response.EnsureSuccessStatusCode();
            var JsonResponse = await response.Content.ReadAsStringAsync();
            var NoPodUsers = JsonSerializer.Deserialize<IEnumerable<AppUserDTO>>(JsonResponse, jsonSerializerOptions).AsQueryable();

            return NoPodUsers;
        }

        public async Task<String> UpdateCurrentUser(UserInfoUpdateDto userInfoUpdateDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("api/users/update", userInfoUpdateDto);
                response.EnsureSuccessStatusCode();
                return "Success";
            }
            catch (Exception e)
            { 
                return e.Message;
            }
        }

        public async Task<string> UpdatePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("api/users/change-password", changePasswordDto); 
                response.EnsureSuccessStatusCode();
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<String> DeleteCurrentUser()
        {
            try
            {
                var response = await _httpClient.DeleteAsync("api/users/delete");
                response.EnsureSuccessStatusCode();
                return "success";
            }
            catch (Exception e)
            {
                return e.Message;
            }


        }
    }
}