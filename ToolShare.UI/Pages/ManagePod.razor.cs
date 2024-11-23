using Microsoft.AspNetCore.Components;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages;

public partial class ManagePod : ComponentBase
{
    private bool _isLoading { get; set; } = true;
    private string? Message { get; set; }
    public PodDTO Pod { get; set; }
    public string NewPodName { get; set; } = string.Empty;
    
    [Parameter]
    public string podName { get; set; }
    [Inject]
    public required IPodsDataService PodsDataService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Pod = await PodsDataService.FindPodDetailsByName(podName);
        NewPodName = Pod.Name;
        _isLoading = false;
    }

    public async Task<String> HandleEditSubmit()
    {
        Message = await PodsDataService.UpdatePodName(Pod.PodId, NewPodName);
        return Message;    
    }
}