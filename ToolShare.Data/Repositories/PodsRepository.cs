using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToolShare.Data.Models;
using ToolShare.Data.Repositories;

namespace ToolShare.Data.Repositories
{
    public class PodsRepository : GenericRepository<Pod>, IPodsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PodsRepository(ApplicationDbContext context,
            UserManager<AppUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddUserToPod(AppUser appUser, Pod pod)
        {
            pod.PodMembers.Add(appUser);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserFromPod(AppUser appUser, Pod pod)
        {
            pod.PodMembers.Remove(appUser);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateName(string newName, Pod pod)
        {
            pod.Name = newName;
            await _context.SaveChangesAsync();
        }

        public async Task ChangeManager(AppUser appUser, Pod pod)
        {
            pod.podManager = appUser;
            await _context.SaveChangesAsync();
        }

        public async Task<Pod> FindPodByName(string podName)
        {
            return await _context.Pods
                .Include(p => p.PodMembers)
                .ThenInclude(u => u.ToolsOwned)
                .Include(p => p.podManager)
                .ThenInclude(u => u.ToolsOwned)
                .FirstOrDefaultAsync(p => p.Name == podName);   
        }

    }
}

