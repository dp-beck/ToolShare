using ToolShare.UI.DTOs;

namespace ToolShare.UI.Services
{
    public interface IToolsDataService
    {
        public Task<IEnumerable<ToolDTO>> GetAllTools();
        public Task<IQueryable<ToolDTO>> GetToolsByPod(int podId);
        public Task<IQueryable<ToolDTO>> GetToolsOwnedByUser(string username);
        public Task<IQueryable<ToolDTO>> GetToolsBorrowedByUser(string username);
        public Task<FormResult> CreateTool(ToolDTO tool);
        public Task<ToolDTO> FindToolById(int toolId);
        public Task<FormResult> UpdateTool(int toolId, UpdateToolDTO updateToolDto);
        public Task<FormResult> RequestTool(int toolId);
        public Task<FormResult> LendTool(int toolId);
        public Task<FormResult> RequestToolReturn(int toolId);
        public Task<FormResult> AcceptToolReturned(int toolId);
        public Task<FormResult> DeleteTool(int toolId);
    }
}