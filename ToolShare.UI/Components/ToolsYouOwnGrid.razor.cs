using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Components;

public partial class ToolsYouOwnGrid : ComponentBase
{
    private string Message { get; set; } = string.Empty;
    private AppUserDTO userInfo {get;set;}
    private IQueryable<ToolDTO> ToolsOwnedQueryable { get; set; }
    private string nameFilter = string.Empty;
    private string statusFilter = string.Empty;
    private PaginationState pagination = new PaginationState { ItemsPerPage = 5 };
    private IQueryable<ToolDTO> filteredOwnedTools
    {
        get
        {
            var result = ToolsOwnedQueryable;
            if (!string.IsNullOrEmpty(nameFilter))
            {
                result = result.Where(x => x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(statusFilter))
            {
                result = result.Where(x => x.ToolStatus.ToString() == statusFilter);
            }
            return result;
        }
    }

    [Inject]
    public required IUsersDataService UsersDataService { get; set; }
    
    [Inject]
    public required IToolsDataService ToolsDataService { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        userInfo = await UsersDataService.GetCurrentUser();
        ToolsOwnedQueryable = await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName);
    }
   
    private async Task HandleAcceptClick(int toolId)
    {
        Message = await ToolsDataService.LendTool(toolId);
        ToolsOwnedQueryable = await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName);
    }
   
    private async Task HandleMarkReturnClick(int toolId)
    {
        // TO DO
    }
    private async Task HandleDeleteClick(int toolId)
    {
        Message = await ToolsDataService.DeleteTool(toolId);
        ToolsOwnedQueryable = await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName);
    }
}