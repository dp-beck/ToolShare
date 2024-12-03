using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Components;

public partial class HomeUser : ComponentBase
{
    private bool IsLoading { get; set; } = true;
    public required AppUserDTO UserInfo {get;set;}
    private IQueryable<ToolDTO>? AllTools { get; set; }
    private IEnumerable<ToolDTO>? FilteredTools { get; set; }
    private string _searchString = String.Empty;


    [Inject]
    public required IUsersDataService UsersDataService { get; set; }

    [Inject]
    public required IToolsDataService ToolsDataService { get; set; }

    [Inject]
    public required ISnackbar Snackbar { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        UserInfo = await UsersDataService.GetCurrentUser();
        AllTools = await ToolsDataService.GetToolsByPod(UserInfo.PodJoinedId);
        FilteredTools = AllTools.ToList();
        IsLoading = false;
    }

    private async Task HandleRequestClick(int toolId)
    {
        var result = await ToolsDataService.RequestTool(toolId);

        if (result.Succeeded)
        {
            AllTools = await ToolsDataService.GetToolsByPod(UserInfo.PodJoinedId);
            FilteredTools = AllTools;
            Snackbar.Add("Tool successfully requested!", Severity.Success);    
        }
        else
        {
            Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
        }
    } 
    
    // quick filter - filter globally across multiple columns with the same input
    private Func<ToolDTO, bool> QuickFilter => tool =>
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;

        if (tool.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;

        if (tool.ToolOwnerName != null && tool.ToolOwnerName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        
        return false;
    };
}