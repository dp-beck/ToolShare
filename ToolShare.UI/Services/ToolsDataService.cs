using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToolShare.UI.Dtos;

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

        public Task<IEnumerable<ToolDto>> GetAllTools()
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<ToolDto>> GetToolsByPod(int podId)
        {
            var response = await _httpClient.GetAsync($"api/tools/tools-by-pod/{podId}");

            response.EnsureSuccessStatusCode();

            var toolsJson = await response.Content.ReadAsStringAsync();
            var toolsInfo = JsonSerializer.Deserialize<IEnumerable<ToolDto>>(toolsJson, jsonSerializerOptions).AsQueryable();

            return toolsInfo;
        }

        public async Task<IQueryable<ToolDto>> GetToolsOwnedByUser(string username)
        {
            var response = await _httpClient.GetAsync($"api/tools/tools-by-user-owned/{username}");
            
            response.EnsureSuccessStatusCode();
            
            var toolsJson = await response.Content.ReadAsStringAsync();
            var toolsInfo = JsonSerializer.Deserialize<IEnumerable<ToolDto>>(toolsJson, jsonSerializerOptions).AsQueryable();
            return toolsInfo;
        }

        public async Task<IQueryable<ToolDto>> GetToolsBorrowedByUser(string username)
        {
            var response = await _httpClient.GetAsync($"api/tools/tools-by-user-borrowed/{username}");
            
            response.EnsureSuccessStatusCode();
            
            var toolsJson = await response.Content.ReadAsStringAsync();
            var toolsInfo = JsonSerializer.Deserialize<IEnumerable<ToolDto>>(toolsJson, jsonSerializerOptions).AsQueryable();
            return toolsInfo;
        }
        
        public async Task<ServiceResult> CreateTool(ToolDto tool)
        {
            string[] defaultDetail = [ "An unknown error prevented tool from being created." ];
            
            var result = await _httpClient.PostAsJsonAsync<ToolDto>("api/tools", tool);

            if (result.IsSuccessStatusCode)
            {
                return new ServiceResult { Succeeded = true };
            }
            
            var details = await result.Content.ReadAsStringAsync();
                
            return new ServiceResult
            {
                Succeeded = false,
                ErrorList = [details]
            };        
        }

        public async Task<ToolDto> FindToolById(int toolId)
        {
            var response = await _httpClient.GetAsync($"api/tools/{toolId}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var toolDetails = JsonSerializer.Deserialize<ToolDto>(jsonResponse, jsonSerializerOptions);
            
            return toolDetails;
        }

        public async Task<ServiceResult> UpdateTool(int toolId, UpdateToolDto updateToolDto)
        {
            string[] defaultDetail = [ "An unknown error prevented tool from being updated." ];

            try
            {
                var result = await _httpClient.PutAsJsonAsync($"api/tools/{toolId}", updateToolDto);
                if (result.IsSuccessStatusCode)
                {
                    return new ServiceResult { Succeeded = true };
                }
                var details = await result.Content.ReadAsStringAsync();
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = [details]
                };
            }
            catch (Exception)
            { 
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };            
            }
        }

        public async Task<ServiceResult> RequestTool(int toolId)
        {
            string[] defaultDetail = [ "An unknown error prevented tool from being requested." ];

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/request-tool");
                using var result = await _httpClient.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    return new ServiceResult { Succeeded = true };
                }
                
                var details = await result.Content.ReadAsStringAsync();
                
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = [details]
                };
            }
            catch (Exception)
            { 
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };
            }
        }

        public async Task<ServiceResult> LendTool(int toolId)
        {
            string[] defaultDetail = [ "An unknown error prevented tool from being lent." ];

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/lend-tool");
                using var result = await _httpClient.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    return new ServiceResult { Succeeded = true };
                }
                
                var details = await result.Content.ReadAsStringAsync();
                
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = [details]
                };

            }
            catch (Exception)
            {
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };
            }
        }

        public async Task<ServiceResult> RejectToolRequest(int toolId)
        {
            {
                string[] defaultDetail = [ "An unknown error prevented tool request from being rejected." ];

                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/reject-tool-request");
                    using var result = await _httpClient.SendAsync(request);
                    if (result.IsSuccessStatusCode)
                    {
                        return new ServiceResult { Succeeded = true };
                    }
                
                    var details = await result.Content.ReadAsStringAsync();
                
                    return new ServiceResult
                    {
                        Succeeded = false,
                        ErrorList = [details]
                    };

                }
                catch (Exception)
                {
                    return new ServiceResult
                    {
                        Succeeded = false,
                        ErrorList = defaultDetail
                    };
                }
            }
        }

        public async Task<ServiceResult> RequestToolReturn(int toolId)
        {
            string[] defaultDetail = [ "An unknown error prevented the return from being requested." ];

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/request-tool-return");
                using var result = await _httpClient.SendAsync(request);
                if (result.IsSuccessStatusCode) return new ServiceResult { Succeeded = true };
                
                var details = await result.Content.ReadAsStringAsync();
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = [details]
                };
            }
            catch (Exception)
            {
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };            
            }        
        }

        public async Task<ServiceResult> AcceptToolReturned(int toolId)
        {
            string[] defaultDetail = [ "An unknown error prevented tool return from being accepted." ];

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/tools/{toolId}/accept-tool-return");
                using var result = await _httpClient.SendAsync(request);
                if (result.IsSuccessStatusCode) return new ServiceResult { Succeeded = true };
                
                var details = await result.Content.ReadAsStringAsync();
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = [details]
                };
            }
            catch (Exception)
            {
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };
            }
        }

        public async Task<ServiceResult> DeleteTool(int toolId)
        {
            string[] defaultDetail = [ "An unknown error prevented the tool from being deleted." ];

            try
            {
                var response = await _httpClient.DeleteAsync($"api/tools/{toolId}");
                if (response.IsSuccessStatusCode) return new ServiceResult { Succeeded = true };
                var details = await response.Content.ReadAsStringAsync();
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = [details]
                };            
            }
            catch (Exception)
            {
                return new ServiceResult
                {
                    Succeeded = false,
                    ErrorList = defaultDetail
                };            
            }
        }
    }
}