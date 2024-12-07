using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;
using ToolShare.UI.Dtos;

namespace ToolShare.UI.Services
{
    public interface IPodsDataService
    {
        public Task<IEnumerable<PodDto>?> GetAllPods();
        public Task<IEnumerable<LimitedPodInfoDto>> GetAllPodsLimitedInfoForNoPodUser();
        public Task<PodDto> FindPodDetailsByName(string podName);
        public Task<PodDto?> FindPodDetailsById(int podId);

        public Task<FormResult> InitializeNewPod(string podName);
        public Task<FormResult> UpdatePodName(int podId, string newPodName);
        public Task<FormResult> AddUser(int podId, string username);
        public Task<FormResult> RemoveUser(int podId, string username);
        public Task<FormResult> ChangeManager(int podId, string username);
        public Task<FormResult> DeletePod(int podId);
    }
}