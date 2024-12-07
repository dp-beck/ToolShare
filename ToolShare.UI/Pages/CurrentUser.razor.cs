using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Extensions;
using MudBlazor;
using ToolShare.Data;
using ToolShare.Data.Models;
using ToolShare.UI.Dtos;
using ToolShare.UI.Services;

namespace ToolShare.UI.Pages
{
    public partial class CurrentUser
    {
        MudForm UserDetailsForm;
        MudForm ChangePasswordForm;
        private bool validUserDetailsForm;
        private bool validChangePasswordForm;
        private string[] UserDetailsFormErrors = [];
        private string[] ChangePasswordFormErrors = [];
        private bool _isLoading {get;set;} = true;
        private AppUserDto userInfo {get;set;}

        [SupplyParameterFromForm]
        private UserInfoUpdateDto? UserEditDTO { get; set; }

        [SupplyParameterFromForm]
        private ChangePasswordDto? ChangePasswordDto { get; set; }

        [Inject] public required IUsersDataService UsersDataService { get; set; }
        [Inject] public required NavigationManager PageManager { get; set; }
        [Inject] public required ISnackbar Snackbar { get; set; }
        
        [Inject] public required IDialogService DialogService { get; set; }

        
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
        
        private async Task HandleEditSubmit()
        {
            var result = await UsersDataService.UpdateCurrentUser(UserEditDTO);
            if (result.Succeeded)
            {    
                Snackbar.Add("User details successfully updated!", Severity.Success);    
            }
            else
            {
                Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
            }
        }

        private async Task HandleChangePassword()
        {
            var result = await UsersDataService.UpdatePassword(ChangePasswordDto);
            if (result.Succeeded)
            {
                Snackbar.Add("Password changed successfully!", Severity.Success);
            }
            else
            {
                Snackbar.Add("Password change failed!", Severity.Error);
            }
        }

        private async Task OnDeleteButtonClick()
        {
            bool? result = await DialogService.ShowMessageBox(
                "Warning", 
                "Deleting can not be undone! Are you sure? Once deleted, you " +
                "will be sent back to the login page.", 
                yesText:"Delete!", cancelText:"Cancel");

            if (result != null && result.Value)
            {
                await HandleDelete();
            }
        }
        private async Task HandleDelete()
        {
            var result = await UsersDataService.DeleteCurrentUser();

            if (result.Succeeded)
            {
                PageManager.NavigateTo("login", forceLoad: true);
            }
            else
            {
                Snackbar.Add($"Error: {result.ErrorList}", Severity.Error);
            }
        }
        
        private IEnumerable<string> PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "Password is required!";
                yield break;
            }
            if (pw.Length < 8)
                yield return "Password must be at least of length 8";
            if (!Regex.IsMatch(pw, @"[A-Z]"))
                yield return "Password must contain at least one capital letter";
            if (!Regex.IsMatch(pw, @"[a-z]"))
                yield return "Password must contain at least one lowercase letter";
            if (!Regex.IsMatch(pw, @"[0-9]"))
                yield return "Password must contain at least one digit";
        }

    }
}