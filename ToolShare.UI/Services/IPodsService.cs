using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.UI.Models;

namespace ToolShare.UI.Services
{
    public interface IPodsService
    {
        public Task<List<Pod>> GetPods();
    }
}