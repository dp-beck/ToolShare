using ToolShare.Data.Models;

namespace ToolShare.Data.Repositories
{
    public interface IToolsRepository : IGenericRepository<Tool>
    {
        public Task<IEnumerable<Tool>> FindToolsOwnedByUsername(string username);
        public Task<IEnumerable<Tool>> FindToolsBorrowedByUsername(string username);
        public Task RequestTool(Tool toolRequested, AppUser toolRequester);
        public Task LendTool(Tool toolBorrowed, AppUser toolBorrower);
        public Task RejectToolRequest(Tool toolRequested);
        public Task AcceptToolReturn(Tool toolReturned);
        public Task RequestToolReturn(Tool toolBorrowed);

    }
}