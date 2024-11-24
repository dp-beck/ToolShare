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
        public Task<String> UpdateCurrentUser(UserInfoUpdateDto userInfoUpdateDto);
        public Task<String> UpdatePassword(ChangePasswordDto changePasswordDto);
        public Task<String> DeleteCurrentUser();
    }
}