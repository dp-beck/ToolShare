using ToolShare.Data.Models;

namespace ToolShare.Data.Repositories
{
    public interface IToolsRepository : IGenericRepository<Tool>
    {
        public Task<IEnumerable<Tool>> FindToolsOwnedByUsername(string username);
        public Task<IEnumerable<Tool>> FindToolsBorrowedByUsername(string username);
    }
}