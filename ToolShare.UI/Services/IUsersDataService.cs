using ToolShare.UI.Dtos;

namespace ToolShare.UI.Services
{
    public interface IUsersDataService
    {
        public Task<AppUserDto> GetCurrentUser();
        public Task<AppUserDto> FindUserByUsername(string username);
        public Task<IQueryable<AppUserDto>> GetNoPodUsers();
        public Task<FormResult> UpdateCurrentUser(UserInfoUpdateDto userInfoUpdateDto);
        public Task<FormResult> UpdatePassword(ChangePasswordDto changePasswordDto);
        public Task<FormResult> DeleteCurrentUser();
    }
}