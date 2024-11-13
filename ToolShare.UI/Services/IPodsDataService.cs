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
        public Task<Pod> GetPodDetails();
        public Task<PodDTO> InitializeNewPod(PodDTO podDTO);

    }
}