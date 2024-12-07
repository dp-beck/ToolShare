using System.Data.SqlTypes;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using ToolShare.Data.Models;
using ToolShare.UI.Dtos;
using ToolShare.UI.Identity.Models;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages;

public partial class ManageTools : ComponentBase
{
    private bool _isLoading { get; set; } = true;
    private string Message { get; set; } = string.Empty;
    private AppUserDto userInfo {get;set;}
    
    private string borrowedToolsNameFilter = string.Empty;
    private string OwnedToolsNameFilter = string.Empty;
    private string statusFilter = string.Empty;
    private IQueryable<ToolDto> ToolsBorrowedQueryable { get; set; }
    private IQueryable<ToolDto> ToolsOwnedQueryable { get; set; }
    private IEnumerable<ToolDto>? ToolsOwned { get; set; }
    private IEnumerable<ToolDto>? ToolsBorrowed { get; set; }
    private string _searchStringOwnedTools = String.Empty;
    private string _searchStringBorrowedTools = String.Empty;

    private IQueryable<ToolDto> filteredBorrowedTools
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
    
    private IQueryable<ToolDto> filteredOwnedTools
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
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }
    protected override async Task OnInitializedAsync()
    {
        userInfo = await UsersDataService.GetCurrentUser();
        ToolsBorrowed = (await ToolsDataService.GetToolsBorrowedByUser(userInfo.UserName)).ToList();
        ToolsOwned = (await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName)).ToList();
        _isLoading = false;
    }
    
    private async Task HandleAcceptClick(int toolId)
    {
        var result = await ToolsDataService.LendTool(toolId);
        
        if (result.Succeeded)
        {
            ToolsOwned = (await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName)).ToList();
            Snackbar.Add("Tool successfully lent!", Severity.Success);    
        }
        else
        {
            Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
        }
    }
   
    private async Task HandleRequestReturnClick(int toolId)
    {
        var result = await ToolsDataService.RequestToolReturn(toolId);
        if (result.Succeeded)
        {
            ToolsBorrowed = (await ToolsDataService.GetToolsBorrowedByUser(userInfo.UserName)).ToList();
            Snackbar.Add("Tool return successfully requested!", Severity.Success);
        }
        else
        {
            Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
        }
    }

    private async Task HandleAcceptReturnClick(int toolId)
    {
        var result = await ToolsDataService.AcceptToolReturned(toolId);
        if (result.Succeeded)
        {
            ToolsOwned = (await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName)).ToList();
            Snackbar.Add("Tool return successfully accepted!", Severity.Success);    
        }
        else
        {
            Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
        }    
    }
    
    private async Task HandleDeleteClick(int toolId)
    {
        var result = await ToolsDataService.DeleteTool(toolId);
        if (result.Succeeded)
        {
            ToolsOwned = (await ToolsDataService.GetToolsOwnedByUser(userInfo.UserName)).ToList();
            Snackbar.Add("Tool successfully deleted!", Severity.Success);    
        }
        else
        {
            Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
        }        
    }
    
    // quick filter for Owned Tools - filter globally across multiple columns with the same input
    private Func<ToolDto, bool> QuickFilterOwnedTools => tool =>
    {
        if (string.IsNullOrWhiteSpace(_searchStringOwnedTools))
            return true;

        if (tool.Name.Contains(_searchStringOwnedTools, StringComparison.OrdinalIgnoreCase))
            return true;

        if (tool.ToolOwnerName != null && tool.ToolOwnerName.Contains(_searchStringOwnedTools, StringComparison.OrdinalIgnoreCase))
            return true;
        
        return false;
    };
    
    // quick filter for Owned Tools - filter globally across multiple columns with the same input
    private Func<ToolDto, bool> QuickFilterBorrowedTools => tool =>
    {
        if (string.IsNullOrWhiteSpace(_searchStringBorrowedTools))
            return true;

        if (tool.Name.Contains(_searchStringBorrowedTools, StringComparison.OrdinalIgnoreCase))
            return true;

        if (tool.ToolOwnerName != null && tool.ToolOwnerName.Contains(_searchStringBorrowedTools, StringComparison.OrdinalIgnoreCase))
            return true;
        
        return false;
    };
}