using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages
{
    public partial class Home()
    {
        public IEnumerable<ToolShare.UI.DTOs.PodDTO> Pods { get; set; }

        [Inject]
        public required IPodsDataService PodsDataService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Pods = await PodsDataService.GetAllPods();
        }
    }
}