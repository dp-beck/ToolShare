using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using ToolShare.Data.Models;
using ToolShare.UI.Dtos;

namespace ToolShare.UI.Services
{
    public class PodsDataService : IPodsDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions jsonSerializerOptions =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = ReferenceHandler.Preserve
            };

        public PodsDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PodDto> FindPodDetailsByName(string podName)
        {
            var response = await _httpClient.GetAsync($"api/Pods/{podName}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var podDetails = JsonSerializer.Deserialize<PodDto>(jsonResponse, jsonSerializerOptions);
            
            return podDetails;
        }
        
        public async Task<PodDto?> FindPodDetailsById(int podId)
        {
            var response = await _httpClient.GetAsync($"api/pods/{podId}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var podDto = JsonSerializer.Deserialize<PodDto>(jsonResponse, jsonSerializerOptions);
            
            return podDto;
        }

        public async Task<IEnumerable<PodDto>?> GetAllPods()
        {

            var podsResponse = await _httpClient.GetAsync("api/pods/");

            podsResponse.EnsureSuccessStatusCode();

            var podsJson = await podsResponse.Content.ReadAsStringAsync();
            var podDtos = JsonSerializer.Deserialize<IEnumerable<PodDto>>(podsJson, jsonSerializerOptions);

            return podDtos;
        }

        public async Task<IEnumerable<LimitedPodInfoDto>> GetAllPodsLimitedInfoForNoPodUser()
        {

            var podsResponse = await _httpClient.GetAsync("api/pods/pod-list-for-nopoduser");

            podsResponse.EnsureSuccessStatusCode();

            var podsJson = await podsResponse.Content.ReadAsStringAsync();
            var podsInfo = JsonSerializer.Deserialize<IEnumerable<LimitedPodInfoDto>>(podsJson, jsonSerializerOptions);

            return podsInfo;
        }

        public async Task<FormResult> InitializeNewPod(string podName)
        {
            string[] defaultDetail = [ "An unknown error prevented pod from being created." ];

            try
            {
                var result = await _httpClient.PostAsJsonAsync("api/pods", podName);
                
                if (result.IsSuccessStatusCode)
                {
                    return new FormResult { Succeeded = true };
                }
                
                // body should contain details about why it failed
                var details = await result.Content.ReadAsStringAsync();
                
                // return the error list
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = [details]
                };
            }
            catch (Exception)
            {
                // unknown error
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };
            }
            
        }

        public async Task<FormResult> UpdatePodName(int podId, string newPodName)
        {
            string[] defaultDetail = [ "An unknown error prevented pod from being renamed." ];

            try
            {
                var result = await _httpClient.PutAsJsonAsync($"api/pods/{podId}/updatename", newPodName);
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
            catch (Exception)
            { 
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };            
            }
        }

        public async Task<FormResult> AddUser(int podId, string username)
        {
            string[] defaultDetail = [ "An unknown error prevented the user from being added." ];

            try
            {
                var result = await _httpClient.PutAsJsonAsync($"api/pods/{podId}/add-user", username);
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
            catch (Exception)
            { 
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };            
            }
        }

        public async Task<FormResult> RemoveUser(int podId, string username)
        {
            string[] defaultDetail = [ "An unknown error prevented the user from being removed." ];

            try
            {
                var result = await _httpClient.PutAsJsonAsync($"api/pods/{podId}/remove-user", username);
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
            catch (Exception)
            {
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };                        
            }
        }

        public async Task<FormResult> ChangeManager(int podId, string username)
        {
            string[] defaultDetail = [ "An unknown error prevented the user from being removed." ];
   
            try
            {
                var result = await _httpClient.PutAsJsonAsync($"api/pods/{podId}/change-pod-manager", username);
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
            catch (Exception)
            {
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };                        
            }
        }

        public async Task<FormResult> DeletePod(int podId)
        {
            string[] defaultDetail = [ "An unknown error prevented the pod from being deleted." ];

            try
            {
                var result = await _httpClient.DeleteAsync("api/pods/delete/{podId}");
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
            catch (Exception)
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