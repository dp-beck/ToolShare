using ToolShare.UI.Dtos;

namespace ToolShare.UI.Services
{
    public interface IToolsDataService
    {
        public Task<IEnumerable<ToolDto>> GetAllTools();
        public Task<IQueryable<ToolDto>> GetToolsByPod(int podId);
        public Task<IQueryable<ToolDto>> GetToolsOwnedByUser(string username);
        public Task<IQueryable<ToolDto>> GetToolsBorrowedByUser(string username);
        public Task<ServiceResult> CreateTool(ToolDto tool);
        public Task<ToolDto> FindToolById(int toolId);
        public Task<ServiceResult> UpdateTool(int toolId, UpdateToolDto updateToolDto);
        public Task<ServiceResult> RequestTool(int toolId);
        public Task<ServiceResult> LendTool(int toolId);
        public Task<ServiceResult> RejectToolRequest(int toolId);
        public Task<ServiceResult> RequestToolReturn(int toolId);
        public Task<ServiceResult> AcceptToolReturned(int toolId);
        public Task<ServiceResult> DeleteTool(int toolId);
    }
}