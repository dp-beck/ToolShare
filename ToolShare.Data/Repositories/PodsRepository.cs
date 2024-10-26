using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ToolShare.Data.Models;

namespace ToolShare.Data.Repositories
{
    public class PodsRepository : IPodsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PodsRepository(ApplicationDbContext context,
            UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task CreatePod(Pod pod)
        {
            _context.Pods.Add(pod);
            await _context.SaveChangesAsync();
        }
    }
}