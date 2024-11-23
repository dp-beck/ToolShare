using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;

namespace ToolShare.Data.Repositories
{
    public interface IToolsRepository : IRepository<Tool>
    {
        public Task<IEnumerable<Tool>> GetToolsOwnedByUsername(string username);
        public Task<IEnumerable<Tool>> GetToolsBorrowedByUsername(string username);
    }
}