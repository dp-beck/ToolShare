using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToolShare.Data.Models;

namespace ToolShare.Data.Repositories
{
    public class ToolsRepository : GenericRepository<Tool>, IToolsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ToolsRepository(ApplicationDbContext context, 
            UserManager<AppUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }


    }
}