using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ToolShare.UI.Dtos;
using ToolShare.UI.Services;

namespace ToolShare.UI.Layout;

public partial class SideBar
{
    private PodDto CurrentPod { get; set; } = new();
    
    [Inject]
    public required IPodsDataService IPodsDataService { get; set; }
    
    public AppUserDto currentUser { get; set; } = new AppUserDto();
    
    [Inject]
    public required IUsersDataService UsersDataService { get; set; }

    
    protected override async Task OnInitializedAsync()
    {
        currentUser = await UsersDataService.GetCurrentUser();
        CurrentPod = await IPodsDataService.FindPodDetailsById(currentUser.PodJoinedId);
    }   
}