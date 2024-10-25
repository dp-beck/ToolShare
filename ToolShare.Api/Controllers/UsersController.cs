using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolShare.Data.Models;
using ToolShare.Api.Dtos;

namespace ToolShare.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager; // will remove once I finish making the repository
        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<AppUserDto>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = users.Select(u => new AppUserDto
            {
                UserName = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                AboutMe = u.AboutMe,
                ProfilePhotoUrl = u.ProfilePhotoUrl
            }).ToList();

            return userDtos;
        }

        [HttpGet]
        [Route("info")]
        [Authorize]
        public async Task<AppUserDto> GetCurrentUserInfo()
        {
            var user = HttpContext.User;
            var appUser = await _userManager.GetUserAsync(user);
            var appUserDto = new AppUserDto
            {
                UserName = appUser.UserName,
                Email = appUser.Email,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                AboutMe = appUser.AboutMe,
                ProfilePhotoUrl = appUser.ProfilePhotoUrl,
            };
            
            return appUserDto;
        }

        [HttpGet]
        [Route("info/{username}")]
        [Authorize]
        public async Task<AppUserDto> GetUserInfoByUsername(string username)
        {
            var appUser = await _userManager.FindByNameAsync(username);
            var appUserDto = new AppUserDto
            {
                UserName = appUser.UserName,
                Email = appUser.Email,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                AboutMe = appUser.AboutMe,
                ProfilePhotoUrl = appUser.ProfilePhotoUrl,
            };
            
            return appUserDto;
        }

        [HttpPost]   
        [Route("register")]
         public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            var user = new AppUser
            {
                UserName = registrationDto.UserName,
                Email = registrationDto.Email,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
            };

            var result = await _userManager.CreateAsync(user, registrationDto.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Registration successful"});
            }
            return BadRequest(new {Errors = result.Errors});
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies)
        {
            var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
            var isPersistent = (useCookies == true) && (useSessionCookies != true);
            _signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, isPersistent, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return BadRequest(new {Errors = result });
            }
            return Ok(new { Message = "Login successful"});
        }

        [HttpPost]   
        [Route("logout")]
        [Authorize]
         public async Task<IResult> Logout([FromBody] object empty)
        {
            if (empty is not null)
            {
                await _signInManager.SignOutAsync();
                return Results.Ok();
            }
            return Results.Unauthorized();
        }

        [HttpPut]
        [Route("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _userManager.ChangePasswordAsync(currentUser, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Password change sucessful"});
            }

            return BadRequest(new {Errors = result.Errors});
        }

        [HttpPut]
        [Route("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(AppUserDto appUserDto)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            currentUser.UserName = appUserDto.UserName;
            currentUser.AboutMe = appUserDto.AboutMe;
            currentUser.Email = appUserDto.Email;
            currentUser.FirstName = appUserDto.FirstName;
            currentUser.LastName = appUserDto.LastName;
            currentUser.ProfilePhotoUrl = appUserDto.ProfilePhotoUrl;

            var result = _userManager.UpdateAsync(currentUser);
            if (result.IsCompletedSuccessfully)
            {
                return Ok(new{ Message = "User Update Successful"});
            }

            return BadRequest(new {Errors = result.Result});
 
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize]
        public async Task<IResult> Delete()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser is not null)
            {   
                await _userManager.DeleteAsync(currentUser);
                return Results.Ok();
            }
            
            return Results.Unauthorized();

        }
    }

}