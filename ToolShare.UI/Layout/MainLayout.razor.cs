using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using ToolShare.UI.DTOs;
using ToolShare.UI.Identity;
using ToolShare.UI.Services;

namespace ToolShare.UI.Layout
{
    public partial class MainLayout
    {
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

    }
}