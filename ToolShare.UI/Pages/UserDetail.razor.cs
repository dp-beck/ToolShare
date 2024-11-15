using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ToolShare.Data;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;
using ToolShare.UI.Identity.Models;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages
{
    public partial class UserDetail
    {
        private bool _isLoading {get;set;} = true;
        private AppUserDTO userInfo {get;set;}
        [SupplyParameterFromForm]
        private UserInfoUpdateDto? UserEditDTO { get; set; }
        
        [Inject]
        public required IUsersDataService UsersDataService { get; set; }
        
        protected string Message = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            userInfo = await UsersDataService.GetCurrentUser();
            UserEditDTO = new UserInfoUpdateDto()
            {
                UserName = userInfo.UserName,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                AboutMe = userInfo.AboutMe,
                Email = userInfo.Email,
            };
            _isLoading = false;
        }
        
        private async Task<String> HandleSubmit()
        {
           Message = await UsersDataService.UpdateCurrentUser(UserEditDTO);
           StateHasChanged();
           return Message;
        }
        

        
    }
}