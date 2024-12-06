using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShare.Data.Models;
using ToolShare.UI.DTOs;

namespace ToolShare.UI.Services
{
    public interface IUsersDataService
    {
        public Task<AppUserDTO> GetCurrentUser();
        public Task<AppUserDTO> FindUserByUsername(string username);
        public Task<IQueryable<AppUserDTO>> GetNoPodUsers();
        public Task<FormResult> UpdateCurrentUser(UserInfoUpdateDto userInfoUpdateDto);
        public Task<FormResult> UpdatePassword(ChangePasswordDto changePasswordDto);
        public Task<FormResult> DeleteCurrentUser();
    }
}