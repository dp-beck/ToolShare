using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;

namespace ToolShare.Data.Repositories
{
    public interface IPodsRepository : IRepository<Pod>
    {
        public Task<Pod> FindPodByName(string podName);
        public Task AddUserToPod(AppUser appUser, Pod pod);
        public Task RemoveUserFromPod(AppUser appUser, Pod pod);
        public Task UpdateName(string name, Pod pod);
        public Task ChangeManager(AppUser appUser, Pod pod);
 
    }
}
