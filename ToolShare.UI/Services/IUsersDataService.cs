using ToolShare.UI.Dtos;

namespace ToolShare.UI.Services
{
    public interface IUsersDataService
    {
        public Task<AppUserDto> GetCurrentUser();
        public Task<AppUserDto> FindUserByUsername(string username);
        public Task<IQueryable<AppUserDto>> GetNoPodUsers();
        public Task<ServiceResult> UpdateCurrentUser(UserInfoUpdateDto userInfoUpdateDto);
        public Task<ServiceResult> UpdatePassword(ChangePasswordDto changePasswordDto);
        public Task<ServiceResult> DeleteCurrentUser();
    }
}