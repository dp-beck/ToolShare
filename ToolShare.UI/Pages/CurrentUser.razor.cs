using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Extensions;
using ToolShare.Data;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;
using ToolShare.UI.Identity.Models;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages
{
    public partial class CurrentUser
    {
        private bool _isLoading {get;set;} = true;
        private AppUserDTO userInfo {get;set;}

        [SupplyParameterFromForm]
        private UserInfoUpdateDto? UserEditDTO { get; set; }

        [SupplyParameterFromForm]
        private ChangePasswordDto? ChangePasswordDto { get; set; }

        [Inject]
        public required IUsersDataService UsersDataService { get; set; }
        [Inject]
        public required NavigationManager PageManager { get; set; }
        
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

            ChangePasswordDto = new ChangePasswordDto()
            {
                CurrentPassword = String.Empty,
                NewPassword = string.Empty
            };
            
            _isLoading = false;
        }
        
        private async Task<String> HandleEditSubmit()
        {
            Message = await UsersDataService.UpdateCurrentUser(UserEditDTO); 
            return Message;
        }

        private async Task<String> HandleChangePassword()
        {
            if (string.IsNullOrWhiteSpace(ChangePasswordDto.CurrentPassword))
            {
                Message = "Please Enter Your Current Password";
                return Message;
            }

            if (string.IsNullOrWhiteSpace(ChangePasswordDto.NewPassword))
            {
                Message = "Please Enter New Password";
                return Message;
            }
            
            Message = await UsersDataService.UpdatePassword(ChangePasswordDto);
            return Message;
        }
        
        private void HandleDelete()
        {
            PageManager.NavigateTo("/current-user/delete");
        }
    }
}