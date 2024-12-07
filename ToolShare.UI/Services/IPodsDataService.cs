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

        public Task<ServiceResult> InitializeNewPod(string podName);
        public Task<ServiceResult> UpdatePodName(int podId, string newPodName);
        public Task<ServiceResult> AddUser(int podId, string username);
        public Task<ServiceResult> RemoveUser(int podId, string username);
        public Task<ServiceResult> ChangeManager(int podId, string username);
        public Task<ServiceResult> DeletePod(int podId);
    }
}