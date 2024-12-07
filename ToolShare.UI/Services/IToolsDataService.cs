using ToolShare.UI.Dtos;

namespace ToolShare.UI.Services
{
    public interface IToolsDataService
    {
        public Task<IEnumerable<ToolDto>> GetAllTools();
        public Task<IQueryable<ToolDto>> GetToolsByPod(int podId);
        public Task<IQueryable<ToolDto>> GetToolsOwnedByUser(string username);
        public Task<IQueryable<ToolDto>> GetToolsBorrowedByUser(string username);
        public Task<FormResult> CreateTool(ToolDto tool);
        public Task<ToolDto> FindToolById(int toolId);
        public Task<FormResult> UpdateTool(int toolId, UpdateToolDto updateToolDto);
        public Task<FormResult> RequestTool(int toolId);
        public Task<FormResult> LendTool(int toolId);
        public Task<FormResult> RequestToolReturn(int toolId);
        public Task<FormResult> AcceptToolReturned(int toolId);
        public Task<FormResult> DeleteTool(int toolId);
    }
}