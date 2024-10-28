using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ToolShare.Data.Models;

namespace ToolShare.Data.Repositories
{
    public class JoinPodRequestsRepository : GenericRepository<JoinPodRequest>, IJoinPodRequestsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public JoinPodRequestsRepository(ApplicationDbContext context,
            UserManager<AppUser> userManager) : base (context)
        {
            _context = context;
            _userManager = userManager;
        }

        

    }
}