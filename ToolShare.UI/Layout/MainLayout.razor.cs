using Microsoft.AspNetCore.Components;
using ToolShare.UI.DTOs;
using ToolShare.UI.Identity;
using ToolShare.UI.Services;

namespace ToolShare.UI.Layout
{
    public partial class MainLayout
    {
        bool _drawerOpen = true;
        public AppUserDTO UserInfo { get; set; } = new AppUserDTO();
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