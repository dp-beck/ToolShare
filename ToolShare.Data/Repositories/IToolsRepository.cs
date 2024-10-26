using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShare.Data.Repositories
{
    public interface IToolsRepository
    {
        public Task<List<Tool>> GetAllTools();
        public Task CreateTool();
    }
}