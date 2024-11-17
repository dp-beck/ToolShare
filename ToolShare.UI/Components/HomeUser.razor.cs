using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;
using ToolShare.UI.Pages;
using ToolShare.UI.Services;

namespace ToolShare.UI.Components;

public partial class HomeUser : ComponentBase
{
    private bool _isLoading { get; set; } = true;
    private AppUserDTO userInfo {get;set;}
    private string nameFilter = string.Empty;
    private PaginationState pagination = new PaginationState { ItemsPerPage = 5 };
    private IQueryable<ToolDTO> allTools { get; set; }
    private IQueryable<ToolDTO> currentUserTools { get; set; }
    private IQueryable<ToolDTO> availableTools { get; set; }
    private IQueryable<ToolDTO> filteredTools { get; set; }

    [Inject]
    public required IUsersDataService UsersDataService { get; set; }
    [Inject]
    public required IPodsDataService PodsDataService { get; set; }
    
    [Inject]
    public required IToolsDataService ToolsDataService { get; set; }
    protected override async Task OnInitializedAsync()
    {
        userInfo = await UsersDataService.GetCurrentUser();
        allTools = await ToolsDataService.GetToolsByPod(userInfo.PodJoinedId);
        filteredTools = allTools;
        currentUserTools = allTools.Where(t => t.ToolOwnerName == userInfo.UserName);
        availableTools = allTools.Where(t => t.ToolStatus == ToolStatus.AvailableForBorrowing);
        _isLoading = false;
    }

    private void ShowCurrentTools()
    {
        filteredTools = currentUserTools;
    }

    private void ShowAllTools()
    {
        filteredTools = allTools;
    }

    private void ShowAvailableTools()
    {
        filteredTools = availableTools;
    }

    private void FilterTools()
    {
        filteredTools = filteredTools.Where(t => t.Name.Contains(nameFilter));
    }
}