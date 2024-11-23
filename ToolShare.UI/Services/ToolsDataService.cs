using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using ToolShare.Data.Models;
using ToolShare.UI.Dtos;
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

        public async Task<IQueryable<ToolDTO>> GetToolsOwnedByUser(string username)
        {
            var response = await _httpClient.GetAsync($"api/tools/tools-by-user-owned/{username}");
            
            response.EnsureSuccessStatusCode();
            
            var toolsJson = await response.Content.ReadAsStringAsync();
            var toolsInfo = JsonSerializer.Deserialize<IEnumerable<ToolDTO>>(toolsJson, jsonSerializerOptions).AsQueryable();
            return toolsInfo;
        }

        public async Task<IQueryable<ToolDTO>> GetToolsBorrowedByUser(string username)
        {
            var response = await _httpClient.GetAsync($"api/tools/tools-by-user-borrowed/{username}");
            
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

        public async Task<String> UpdateTool(int toolId, UpdateToolDTO updateToolDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/tools/{toolId}", updateToolDto);
                response.EnsureSuccessStatusCode();
                return "Success";
            }
            catch (Exception e)
            { 
                return e.Message;
            }
        }

        public async Task<String> RequestTool(int toolId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/request-tool");
                using var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return "Success";
            }
            catch (Exception e)
            {
              return e.Message;
            }

        }

        public async Task<String> LendTool(int toolId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/lend-tool");
                using var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> RequestToolReturn(int toolId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/request-tool-return");
                using var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }        
        }

        public async Task<string> AcceptToolReturned(int toolId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/accept-tool-return");
                using var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }

        public async Task<String> DeleteTool(int toolId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/tools/{toolId}");
                response.EnsureSuccessStatusCode();
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}