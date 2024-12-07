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
        public Task<PodDTO> FindPodDetailsByName(string podName);
        public Task<PodDTO> FindPodDetailsById(int podId);

        public Task<FormResult> InitializeNewPod(string podName);
        public Task<FormResult> UpdatePodName(int podId, string newPodName);
        public Task<FormResult> AddUser(int podId, string username);
        public Task<FormResult> RemoveUser(int podId, string username);
        public Task<FormResult> ChangeManager(int podId, string username);
        public Task<FormResult> DeletePod(int podId);
    }
}