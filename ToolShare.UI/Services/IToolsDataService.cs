using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.UI.Dtos;
using ToolShare.UI.DTOs;

namespace ToolShare.UI.Services
{
    public interface IToolsDataService
    {
        public Task<IEnumerable<ToolDTO>> GetAllTools();
        public Task<IQueryable<ToolDTO>> GetToolsByPod(int podId);
        public Task<ToolDTO> CreateTool(ToolDTO tool);
        public Task<ToolDTO> FindToolById(int toolId);
        public Task<String> UpdateTool(int toolId, UpdateToolDTO updateToolDto);
        public Task<String> RequestTool(int toolId);

    }
}