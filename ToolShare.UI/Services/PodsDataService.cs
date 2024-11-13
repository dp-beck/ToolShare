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
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;
using ToolShare.UI.Identity.Models;

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

        public Task<Pod> GetPodDetails()
        {
            throw new NotImplementedException();
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

        public async Task<PodDTO> InitializeNewPod(PodDTO podDTO)
        {
            var podJson = 
                new StringContent(JsonSerializer.Serialize(podDTO), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/pods", podJson);

            if (response.IsSuccessStatusCode)
            {
                
                return await JsonSerializer.DeserializeAsync<PodDTO>(await response.Content.ReadAsStreamAsync());
            }
            
            return null;

        }
    }
}