using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Components;

public partial class ToolsYouBorrowedGrid : ComponentBase
{
    private string Message { get; set; } = string.Empty;
    private AppUserDTO userInfo {get;set;}
    private IQueryable<ToolDTO> ToolsBorrowedQueryable { get; set; }
    private string nameFilter = string.Empty;
    private string statusFilter = string.Empty;
    private PaginationState pagination = new PaginationState { ItemsPerPage = 5 };
    private IQueryable<ToolDTO> filteredBorrowedTools
    {
        get
        {
            var result = ToolsBorrowedQueryable;
            if (!string.IsNullOrEmpty(nameFilter))
            {
                result = result.Where(x => x.Name.Contains(nameFilter, StringComparison.CurrentCultureIgnoreCase));
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
        ToolsBorrowedQueryable = await ToolsDataService.GetToolsBorrowedByUser(userInfo.UserName);
    }
    
    private async Task HandleRequestReturnClick(int toolId)
    {
        // TO DO
    }

}
