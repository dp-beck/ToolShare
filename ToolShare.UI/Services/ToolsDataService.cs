using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;

namespace ToolShare.UI.Services
{
    public class ToolsDataService : IToolsDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly JsonSerializerOptions jsonSerializerOptions =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = ReferenceHandler.Preserve
            };

        public ToolsDataService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public Task<IEnumerable<ToolDTO>> GetAllTools()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<ToolDTO>> GetToolsByPod(int podId)
        {
            var response = await _httpClient.GetAsync($"api/tools/tools-by-pod/{podId}");

            response.EnsureSuccessStatusCode();

            var toolsJson = await response.Content.ReadAsStringAsync();
            var toolsInfo = JsonSerializer.Deserialize<IEnumerable<ToolDTO>>(toolsJson, jsonSerializerOptions).AsQueryable();

            return toolsInfo;
        }

        public async Task<ToolDTO> CreateTool(ToolDTO tool)
        {
            var toolJson = 
                new StringContent(JsonSerializer.Serialize(tool), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/tools", toolJson);

            if (response.IsSuccessStatusCode)
            {
                
                return await JsonSerializer.DeserializeAsync<ToolDTO>(await response.Content.ReadAsStreamAsync());
            }
            
            return null;
        }

        public async Task<ToolDTO> FindToolById(int toolId)
        {
            var response = await _httpClient.GetAsync($"api/tools/{toolId}");
            response.EnsureSuccessStatusCode();
            var JsonResponse = await response.Content.ReadAsStringAsync();
            var toolDetails = JsonSerializer.Deserialize<ToolDTO>(JsonResponse, jsonSerializerOptions);
            
            return toolDetails;
        }
    }
}