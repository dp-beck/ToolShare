using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToolShare.Data.Models;

namespace ToolShare.Data.Repositories
{
    public class ToolsRepository : IToolsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ToolsRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task CreateTool()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Tool>> GetAllTools()
        {
            return await _context.Tools.ToListAsync();
        }
    }
}