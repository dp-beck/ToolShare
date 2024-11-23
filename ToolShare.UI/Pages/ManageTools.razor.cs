using System.Data.SqlTypes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;
using ToolShare.UI.Identity.Models;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages;

public partial class ManageTools : ComponentBase
{
    private bool _isLoading { get; set; } = true;
    private string Message { get; set; } = string.Empty;
    private AppUserDTO userInfo {get;set;}
    
    private PaginationState pagination = new PaginationState { ItemsPerPage = 5 };
    private string borrowedToolsNameFilter = string.Empty;
    private string OwnedToolsNameFilter = string.Empty;
    private string statusFilter = string.Empty;
    private IQueryable<ToolDTO> ToolsBorrowedQueryable { get; set; }
    private IQueryable<ToolDTO> ToolsOwnedQueryable { get; set; }

    private IQueryable<ToolDTO> filteredBorrowedTools
    {
        get
        {
            var result = ToolsBorrowedQueryable;
            if (!string.IsNullOrEmpty(borrowedToolsNameFilter))
            {
                result = result.Where(x => x.Name.Contains(borrowedToolsNameFilter, StringComparison.CurrentCultureIgnoreCase));
            }

            return result;
        }
    }
    
    private IQueryable<ToolDTO> filteredOwnedTools
    {
        get
        {
            var result = ToolsOwnedQueryable;
            if (!string.IsNullOrEmpty(OwnedToolsNameFilter))
            {
                result = result.Where(x => x.Name.Contains(OwnedToolsNameFilter, StringComparison.CurrentCultureIgnoreCase));
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
        ToolsBorrowedQueryable = await ToolsDataService.GetToolsBorrowedByUser(userInfo.UserName);
        ToolsOwnedQueryable = await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName);
        _isLoading = false;
    }
    
    private async Task HandleAcceptClick(int toolId)
    {
        Message = await ToolsDataService.LendTool(toolId);
        ToolsOwnedQueryable = await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName);
        ToolsBorrowedQueryable = await ToolsDataService.GetToolsBorrowedByUser(userInfo.UserName);    
    }
   
    private async Task HandleRequestReturnClick(int toolId)
    {
        Message = await ToolsDataService.RequestToolReturn(toolId);
        ToolsOwnedQueryable = await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName);
        ToolsBorrowedQueryable = await ToolsDataService.GetToolsBorrowedByUser(userInfo.UserName);    
    }

    private async Task HandleAcceptReturnClick(int toolId)
    {
        Message = await ToolsDataService.AcceptToolReturned(toolId);
        ToolsOwnedQueryable = await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName);  
        ToolsBorrowedQueryable = await ToolsDataService.GetToolsBorrowedByUser(userInfo.UserName);    
    }
    
    private async Task HandleDeleteClick(int toolId)
    {
        Message = await ToolsDataService.DeleteTool(toolId);
        ToolsOwnedQueryable = await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName);
    }
}