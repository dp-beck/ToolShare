using Microsoft.EntityFrameworkCore;
using ToolShare.Data.Models;

namespace ToolShare.Data.Repositories
{
    public class ToolsRepository : GenericRepository<Tool>, IToolsRepository
    {
        private readonly ApplicationDbContext _context;

        public ToolsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Tool>> FindToolsOwnedByUsername(string username)
        {
            return await _context.Tools.Where(t => t.ToolOwner.UserName == username)
                .Include(t=>t.ToolOwner)
                .Include(t => t.ToolBorrower)
                .Include(t => t.ToolRequester)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tool>> FindToolsBorrowedByUsername(string username)
        {
            return await _context.Tools.Where(t => t.ToolBorrower != null && t.ToolBorrower.UserName == username)
                .Include(t=>t.ToolOwner)
                .Include(t => t.ToolBorrower)
                .Include(t => t.ToolRequester)
                .ToListAsync();
        }
    }
}