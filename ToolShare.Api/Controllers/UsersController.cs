using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolShare.Data.Models;
using ToolShare.Api.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ToolShare.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        public UsersController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            IMapper mapper
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<AppUserDto>>> GetAllUsers()
        {
            try 
            {
            var users = await _userManager.Users.ToListAsync();
            List<AppUserDto> userDtos = _mapper.Map<List<AppUserDto>>(users);
            return userDtos;
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("currentuser")]
        [Authorize]
        public async Task<ActionResult<AppUserDto>> GetCurrentUserInfo()
        {
            try
            {
            var user = HttpContext.User;
            var appUser = await _userManager.GetUserAsync(user);
            
            var appUserId = await _userManager.GetUserIdAsync(appUser);
            var currentUser = _userManager.Users
                .Include(u => u.PodJoined)
                .Include(u => u.PodManaged)
                .Include(u => u.ToolsOwned)
                .FirstOrDefault(x => x.Id == appUserId);
            
            AppUserDto appUserDto = _mapper.Map<AppUserDto>(currentUser);
                        
            return appUserDto;
            } catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("info/{username}")]
        [Authorize]
        public async Task<ActionResult<AppUserDto>> FindUserInfoByUsername(string username)
        {
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser is null)
                return BadRequest(new { message = "User not found" });

            AppUserDto appUserDto = _mapper.Map<AppUserDto>(appUser);
            
            return appUserDto;
        }

        [HttpGet]
        [Route("users-without-pods")]
        [Authorize(Roles = "PodManager")]
        public async Task<ActionResult<AppUserDto>> GetAllUsersWithoutPods()
        {
            try 
            {
                var users = await _userManager.Users
                    .Where(u => u.PodJoinedId == null)
                    .ToListAsync();

                List<AppUserDto> userDtos = _mapper.Map<List<AppUserDto>>(users);
                return Ok(userDtos);

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
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
                await _userManager.AddToRoleAsync(user, "NoPodUser");
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

            if (result.Succeeded)
            {
                return Ok(new { Message = "Login successful."});
            }
            
            return BadRequest(new { Message = "Login unsucessful. Either the password or username is incorrect."} );
        }

        [HttpPost]   
        [Route("logout")]
        [Authorize]
        public async Task<IResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Results.Ok();
            } catch (Exception e)
            {
                return Results.BadRequest(e);
            }
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
        public async Task<IActionResult> Delete()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser is null)
               return BadRequest(new{ Message = "There is no current user to delete."});

            var result = await _userManager.DeleteAsync(currentUser);

            if (result.Succeeded)
                return Ok(new { Message = "Current user deleted sucessfully."});
            
            return BadRequest(new { Errors = result.Errors});
        }
    }

}