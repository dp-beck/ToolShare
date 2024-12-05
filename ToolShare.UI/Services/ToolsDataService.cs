using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MudBlazor;
using ToolShare.Data.Models;
using ToolShare.UI.Dtos;
using ToolShare.UI.DTOs;
using ToolShare.UI.Identity.Models;

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
        
        public async Task<FormResult> CreateTool(ToolDTO tool)
        {
            string[] defaultDetail = [ "An unknown error prevented tool from being created." ];
            
            var result = await _httpClient.PostAsJsonAsync<ToolDTO>("api/tools", tool);

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

        public async Task<FormResult> RequestTool(int toolId)
        {
            string[] defaultDetail = [ "An unknown error prevented tool from being requested." ];

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/request-tool");
                using var result = await _httpClient.SendAsync(request);
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

        public async Task<FormResult> LendTool(int toolId)
        {
            string[] defaultDetail = [ "An unknown error prevented tool from being lent." ];

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/lend-tool");
                using var result = await _httpClient.SendAsync(request);
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

        public async Task<FormResult> RequestToolReturn(int toolId)
        {
            string[] defaultDetail = [ "An unknown error prevented the return from being requested." ];

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/request-tool-return");
                using var result = await _httpClient.SendAsync(request);
                if (result.IsSuccessStatusCode) return new FormResult { Succeeded = true };
                
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

        public async Task<FormResult> AcceptToolReturned(int toolId)
        {
            string[] defaultDetail = [ "An unknown error prevented tool return from being accepted." ];

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/accept-tool-return");
                using var result = await _httpClient.SendAsync(request);
                if (result.IsSuccessStatusCode) return new FormResult { Succeeded = true };
                
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

        public async Task<FormResult> DeleteTool(int toolId)
        {
            string[] defaultDetail = [ "An unknown error prevented the tool from being deleted." ];

            try
            {
                var response = await _httpClient.DeleteAsync($"api/tools/{toolId}");
                if (response.IsSuccessStatusCode) return new FormResult { Succeeded = true };
                var details = await response.Content.ReadAsStringAsync();
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