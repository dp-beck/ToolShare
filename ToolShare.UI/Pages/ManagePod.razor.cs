using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToolShare.UI.Dtos;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages;

public partial class ManagePod : ComponentBase
{
    private bool _isLoading { get; set; } = true;
    private bool ValidUpdatePodForm;
    private string[] UpdatePodFormErrors = [];
    private string? Message { get; set; }
    public PodDto Pod { get; set; }
    public string NewPodName { get; set; } = string.Empty;
    public string NewPodManagerName { get; set; } = string.Empty;
    private IEnumerable<AppUserDto> NoPodUsers { get; set; }
    
    [Parameter]
    public int podId { get; set; }
    [Inject] public required IPodsDataService PodsDataService { get; set; }
    [Inject] public required IUsersDataService UsersDataService { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required ISnackbar Snackbar { get; set; }
    [Inject] public required IDialogService DialogService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Pod = await PodsDataService.FindPodDetailsById(podId);
        NewPodName = Pod.Name;
        NewPodManagerName = Pod.podManager.UserName;
        NoPodUsers = (await UsersDataService.GetNoPodUsers()).ToList();
        _isLoading = false;
    }

    public async Task HandleEditSubmit()
    {
        
        var result = await PodsDataService.UpdatePodName(Pod.PodId, NewPodName);
        if (result.Succeeded)
            Snackbar.Add("Pod Name Successfully Changed!", Severity.Success);
        else
            Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
    }

    private async Task HandleAddUserClick(string username)
    {
        var result = await PodsDataService.AddUser(Pod.PodId, username);
        if (result.Succeeded)
        {
            Pod = await PodsDataService.FindPodDetailsById(podId);
            NoPodUsers = (await UsersDataService.GetNoPodUsers()).ToList();
            Snackbar.Add("User Added Successfully!", Severity.Success);

        }
        else
        {
            Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
        }
    }

    private async Task HandleRemoveMemberClick(string username)
    {
        var result = await PodsDataService.RemoveUser(Pod.PodId, username);
        if (result.Succeeded)
        {
            Pod = await PodsDataService.FindPodDetailsById(podId);
            NoPodUsers = (await UsersDataService.GetNoPodUsers()).ToList();
            Snackbar.Add("User Removed Successfully!", Severity.Success);
        }
        else
        {
            Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
        }
    }

    private async Task HandleChangeManagerClick()
    {
        var result = await PodsDataService.ChangeManager(Pod.PodId, NewPodManagerName);
        if (result.Succeeded)
        {
            NavigationManager.NavigateTo("", forceLoad: true);
        }
        else
        {
            Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
        }    
    }

    private async Task HandleDelete()
    {
        var result = await PodsDataService.DeletePod(Pod.PodId);

        if (result.Succeeded)
        {
            NavigationManager.NavigateTo("", forceLoad: true);
        }
        else
        {
            Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
        }

    }

    private async Task OnDeleteButtonClick()
    {
        bool? result = await DialogService.ShowMessageBox(
            "Warning", 
            "Deleting can not be undone! Are you sure? Once deleted, you " +
            "will be sent back to the login page.", 
            yesText:"Delete!", cancelText:"Cancel");

        if (result != null && result.Value)
        {
            await HandleDelete();
        }
    }
}