using Microsoft.AspNetCore.Components;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;
using ToolShare.UI.Pages;
using ToolShare.UI.Services;

namespace ToolShare.UI.Components;

public partial class HomeUser : ComponentBase
{
    private string Message { get; set; } = String.Empty;
    private bool _isLoading { get; set; } = true;
    private AppUserDTO userInfo {get;set;}
    private string nameFilter = string.Empty;
    private IQueryable<ToolDTO> allTools { get; set; }
    private IQueryable<ToolDTO> currentUserTools { get; set; }
    private IQueryable<ToolDTO> availableTools { get; set; }
    private IEnumerable<ToolDTO> filteredTools { get; set; }

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
        filteredTools = allTools.ToList();
        currentUserTools = allTools.Where(t => t.ToolOwnerName == userInfo.UserName);
        availableTools = allTools.Where(t => t.ToolStatus == ToolStatus.Available);
        _isLoading = false;
    }

    private async Task<String> HandleRequestClick(int toolId)
    {
        Message = await ToolsDataService.RequestTool(toolId);
        allTools = await ToolsDataService.GetToolsByPod(userInfo.PodJoinedId);
        filteredTools = allTools;
        currentUserTools = allTools.Where(t => t.ToolOwnerName == userInfo.UserName);
        availableTools = allTools.Where(t => t.ToolStatus == ToolStatus.Available);        
        return Message;
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