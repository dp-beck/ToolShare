using Microsoft.AspNetCore.Components;
using ToolShare.UI.DTOs;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages;

public partial class UserDetail : ComponentBase
{
    private bool _isLoading {get;set;} = true;
    public AppUserDTO UserInfo { get; set; }
    [Parameter]
    public string? Username { get; set; }
    
    [Inject]
    IUsersDataService UsersDataService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        UserInfo = await UsersDataService.FindUserByUsername(Username);
        _isLoading = false;
    }
    
    protected override async Task OnParametersSetAsync()
    {
        _isLoading = true;
        UserInfo = await UsersDataService.FindUserByUsername(Username);
        _isLoading = false;
    }
}