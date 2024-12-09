using System.Security.Cryptography;
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

        public async Task RequestTool(Tool toolRequested, AppUser toolRequester)
        {
            toolRequested.ToolStatus = ToolStatus.Requested;
            toolRequested.ToolRequester = toolRequester;
            await _context.SaveChangesAsync();
        }

        public async Task LendTool(Tool toolBorrowed, AppUser toolBorrower)
        {
            toolBorrowed.ToolStatus = ToolStatus.Borrowed;
            toolBorrowed.ToolRequester = null;
            toolBorrowed.ToolBorrower = toolBorrower;
            toolBorrowed.DateBorrowed = DateOnly.FromDateTime(DateTime.Now);
            await _context.SaveChangesAsync();
        }

        public async Task RejectToolRequest(Tool toolRequested)
        {
            toolRequested.ToolRequester = null;
            toolRequested.ToolStatus = ToolStatus.Available;
            await _context.SaveChangesAsync();
        }

        public async Task AcceptToolReturn(Tool toolReturned)
        {
            toolReturned.ToolBorrower = null;
            toolReturned.BorrowerId = null;
            toolReturned.ToolStatus = ToolStatus.Available;
            toolReturned.DateBorrowed = null;
            await _context.SaveChangesAsync();        
        }

        public async Task RequestToolReturn(Tool toolBorrowed)
        {
            toolBorrowed.ToolStatus = ToolStatus.ReturnPending;
            await _context.SaveChangesAsync();
        }
    }
}