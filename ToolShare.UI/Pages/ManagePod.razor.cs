using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
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
    public string NewPodManagerName { get; set; } = string.Empty;
    private IQueryable<AppUserDTO> NoPodUsers { get; set; }
    
    [Parameter]
    public int podId { get; set; }
    [Inject]
    public required IPodsDataService PodsDataService { get; set; }
    [Inject]
    public required IUsersDataService UsersDataService { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Pod = await PodsDataService.FindPodDetailsById(podId);
        NewPodName = Pod.Name;
        NewPodManagerName = Pod.podManager.UserName;
        NoPodUsers = await UsersDataService.GetNoPodUsers();
        _isLoading = false;
    }

    public async Task<String> HandleEditSubmit()
    {
        Message = await PodsDataService.UpdatePodName(Pod.PodId, NewPodName);
        await OnInitializedAsync();
        return Message;    
    }

    private async Task<String> HandleAddUserClick(string username)
    {
        Message = await PodsDataService.AddUser(Pod.PodId, username);
        NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
        return Message;    
    }

    private async Task<String> HandleRemoveMemberClick(string username)
    {
        Message = await PodsDataService.RemoveUser(Pod.PodId, username);
        NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
        return Message;
    }

    private async Task<string> HandleChangeManagerClick()
    {
        Message = await PodsDataService.ChangeManager(Pod.PodId, NewPodManagerName);
        NavigationManager.NavigateTo("", forceLoad: true);
        return Message;
    }

    private async Task HandleDelete()
    {
        Message = await PodsDataService.DeletePod(Pod.PodId);

        if (Message == "success")
        {
            NavigationManager.NavigateTo("", forceLoad: true);
        }

    }
}