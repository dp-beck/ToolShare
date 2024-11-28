using Microsoft.EntityFrameworkCore;
using ToolShare.Data.Models;

namespace ToolShare.Data.Repositories
{
    public class PodsRepository : GenericRepository<Pod>, IPodsRepository
    {
        private readonly ApplicationDbContext _context;

        public PodsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
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
            pod.PodManager = appUser;
            await _context.SaveChangesAsync();
        }

        public async Task<Pod?> FindPodByName(string podName)
        {
            return await _context.Pods
                .Include(p => p.PodMembers)
                .ThenInclude(u => u.ToolsOwned)
                .Include(p => p.PodManager)
                .ThenInclude(u => u.ToolsOwned)
                .FirstOrDefaultAsync(p => p.Name == podName);   
        }

    }
}

