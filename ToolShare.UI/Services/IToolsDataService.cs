using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.UI.DTOs;

namespace ToolShare.UI.Services
{
    public interface IToolsDataService
    {
        public Task<IEnumerable<ToolDTO>> GetAllTools();
        public Task<IQueryable<ToolDTO>> GetToolsByPod(int podId);

    }
}