using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;
using ToolShare.UI.Identity.Models;

namespace ToolShare.UI.Services
{
    public interface IPodsDataService
    {
        public Task<IEnumerable<PodDTO>> GetAllPods();
        public Task<IEnumerable<LimitedPodInfoDTO>> GetAllPodsLimitedInfoForNoPodUser();
        public Task<PodDTO> FindPodDetailsByName(string PodName);
        public Task<PodDTO> FindPodDetailsById(int podId);

        public Task<PodDTO> InitializeNewPod(PodDTO podDTO);
        public Task<String> UpdatePodName(int podId, string NewPodName);
        public Task<String> AddUser(int podId, string username);
        public Task<String> RemoveUser(int podId, string username);
        public Task<String> ChangeManager(int podId, string username);
        public Task<String> DeletePod(int podId);
    }
}