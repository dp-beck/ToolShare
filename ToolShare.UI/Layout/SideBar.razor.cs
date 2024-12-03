using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Layout;

public partial class SideBar
{
    private PodDTO CurrentPod { get; set; } = new();
    
    [Inject]
    public required IPodsDataService IPodsDataService { get; set; }
    
    public AppUserDTO currentUser { get; set; } = new AppUserDTO();
    
    [Inject]
    public required IUsersDataService UsersDataService { get; set; }

    
    protected override async Task OnInitializedAsync()
    {
        currentUser = await UsersDataService.GetCurrentUser();
        CurrentPod = await IPodsDataService.FindPodDetailsById(currentUser.PodJoinedId);
    }   
}