using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;

namespace ToolShare.Data.Repositories
{
    public interface IPodsRepository : IRepository<Pod>
    {
        public Task AddUserToPod(AppUser appUser, Pod pod);
        public Task<Pod> FindPodByName(string podName);
    }
}