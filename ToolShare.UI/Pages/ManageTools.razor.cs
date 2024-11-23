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
    private AppUserDTO userInfo {get;set;}
    

    [Inject]
    public required IUsersDataService UsersDataService { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        userInfo = await UsersDataService.GetCurrentUser();
        _isLoading = false;
    }
}