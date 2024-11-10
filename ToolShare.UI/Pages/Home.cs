using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages
{
    public partial class Home(IPodsService podsService)
    {
        protected override async Task OnInitializedAsync()
        {
            var pods = await podsService.GetPods();
            return base.OnInitializedAsync();
        }
    }
}