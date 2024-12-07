using Microsoft.AspNetCore.Components;
using ToolShare.UI.Dtos;
using ToolShare.UI.Identity;
using ToolShare.UI.Services;

namespace ToolShare.UI.Layout
{
    public partial class MainLayout
    {
        bool _drawerOpen = true;
        public AppUserDto UserInfo { get; set; } = new AppUserDto();
        [Inject]
        public required IUsersDataService UsersDataService { get; set; }
        
        [Inject]
        public required IAccountManagement CookieAuthenticationStateProvider { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (await CookieAuthenticationStateProvider.CheckAuthenticatedAsync())
                UserInfo = await UsersDataService.GetCurrentUser();
        }
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

    }
}