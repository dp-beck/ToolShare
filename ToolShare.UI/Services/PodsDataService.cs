using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;

namespace ToolShare.UI.Services
{
    public class PodsDataService : IPodsDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly JsonSerializerOptions jsonSerializerOptions =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = ReferenceHandler.Preserve
            };

        public PodsDataService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<PodDTO> FindPodDetailsByName(string podName)
        {
            var response = await _httpClient.GetAsync($"api/Pods/{podName}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var podDetails = JsonSerializer.Deserialize<PodDTO>(jsonResponse, jsonSerializerOptions);
            
            return podDetails;
        }
        
        public async Task<PodDTO> FindPodDetailsById(int podId)
        {
            var response = await _httpClient.GetAsync($"api/pods/{podId}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var podDetails = JsonSerializer.Deserialize<Pod>(jsonResponse, jsonSerializerOptions);
            
            PodDTO podDto = _mapper.Map<PodDTO>(podDetails);

            return podDto;
        }

        public async Task<IEnumerable<PodDTO>> GetAllPods()
        {

            var podsResponse = await _httpClient.GetAsync("api/pods/");

            podsResponse.EnsureSuccessStatusCode();

            var podsJson = await podsResponse.Content.ReadAsStringAsync();
            var podsInfo = JsonSerializer.Deserialize<IEnumerable<Pod>>(podsJson, jsonSerializerOptions);

            List<PodDTO> podDTOs = _mapper.Map<List<PodDTO>>(podsInfo);


            return podDTOs;

        }

        public async Task<IEnumerable<LimitedPodInfoDTO>> GetAllPodsLimitedInfoForNoPodUser()
        {

            var podsResponse = await _httpClient.GetAsync("api/pods/pod-list-for-nopoduser");

            podsResponse.EnsureSuccessStatusCode();

            var podsJson = await podsResponse.Content.ReadAsStringAsync();
            var podsInfo = JsonSerializer.Deserialize<IEnumerable<LimitedPodInfoDTO>>(podsJson, jsonSerializerOptions);

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