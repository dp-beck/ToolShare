using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToolShare.UI.DTOs;

namespace ToolShare.UI.Services
{
    public class ToolsDataService : IToolsDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions jsonSerializerOptions =
            new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = ReferenceHandler.Preserve
            };

        public ToolsDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var toolDetails = JsonSerializer.Deserialize<ToolDTO>(jsonResponse, jsonSerializerOptions);
            
            return toolDetails;
        }

        public async Task<FormResult> UpdateTool(int toolId, UpdateToolDTO updateToolDto)
        {
            string[] defaultDetail = [ "An unknown error prevented tool from being updated." ];

            try
            {
                var result = await _httpClient.PutAsJsonAsync($"api/tools/{toolId}", updateToolDto);
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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