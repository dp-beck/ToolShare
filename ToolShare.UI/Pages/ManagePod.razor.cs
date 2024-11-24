using Microsoft.AspNetCore.Components;
using ToolShare.Data.Models;
using ToolShare.UI.Components;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages;

public partial class ManagePod : ComponentBase
{
    private bool _isLoading { get; set; } = true;
    private string? Message { get; set; }
    public PodDTO Pod { get; set; }
    public string NewPodName { get; set; } = string.Empty;
    private IQueryable<AppUserDTO> NoPodUsers { get; set; }
    [Parameter]
    public int podId { get; set; }
    [Inject]
    public required IPodsDataService PodsDataService { get; set; }
    [Inject]
    public required IUsersDataService UsersDataService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Pod = await PodsDataService.FindPodDetailsById(podId);
        NewPodName = Pod.Name;
        NoPodUsers = await UsersDataService.GetNoPodUsers();
        _isLoading = false;
    }

    public async Task<String> HandleEditSubmit()
    {
        Message = await PodsDataService.UpdatePodName(Pod.PodId, NewPodName);
        await OnInitializedAsync();
        return Message;    
    }
}