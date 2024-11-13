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
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> GetAllUsers()
        {
            try 
            {
                var users = await _userManager.Users
                    .Include(u => u.PodJoined)
                    .Include(u => u.PodManaged)
                    .Include(u => u.ToolsOwned)
                    .Include(u => u.ToolsBorrowed)
                    .ToListAsync();

                List<AppUserDto> userDtos = _mapper.Map<List<AppUserDto>>(users);
                return Ok(userDtos);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet]
        [Route("current-user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            try
            {
            var appUser = await _userManager.GetUserAsync(HttpContext.User);
            if (appUser is null) return BadRequest(new {Message = "No current user is logged in."});
        
            var appUserId = await _userManager.GetUserIdAsync(appUser);
            var currentUser = _userManager.Users
                .Include(u => u.PodJoined)
                .Include(u => u.PodManaged)
                .Include(u => u.ToolsOwned)
                .FirstOrDefault(x => x.Id == appUserId);
            
            AppUserDto appUserDto = _mapper.Map<AppUserDto>(currentUser);
                        
            return Ok(appUserDto);
            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet]
        [Route("{username}")]
        [Authorize]
        public async Task<IActionResult> FindUserInfoByUsername(string username)
        {
            try
            {
                var appUser = await _userManager.FindByNameAsync(username);

                if (appUser is null) return BadRequest(new { message = "User not found" });

                var user = _userManager.Users
                    .Include(u => u.PodJoined)
                    .Include(u => u.PodManaged)
                    .Include(u => u.ToolsOwned)
                    .FirstOrDefault(x => x.UserName == username);

                AppUserDto appUserDto = _mapper.Map<AppUserDto>(user);

                return Ok(appUserDto);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
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
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet]
        [Route("current-user/roles")]
        [Authorize]
        public IResult GetRoles()
        {
            var user = HttpContext.User;
            try
            {
                if (user.Identity is not null && user.Identity.IsAuthenticated)
                {
                    var identity = (ClaimsIdentity)user.Identity;
                    var roles = identity.FindAll(identity.RoleClaimType)
                        .Select(c => 
                            new
                        {
                            c.Issuer, 
                            c.OriginalIssuer, 
                            c.Type, 
                            c.Value, 
                            c.ValueType
                        });

                return TypedResults.Json(roles);
                }

                return Results.Unauthorized();
        
            }
            catch (Exception)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]   
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            try
            {
                var user = new AppUser
                {
                UserName = registrationDto.UserName,
                Email = registrationDto.Email,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                AboutMe = registrationDto.AboutMe,
                };

                var result = await _userManager.CreateAsync(user, registrationDto.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "NoPodUser");
                    return Ok(new { Message = "Registration successful"});
                }

                return BadRequest(new {Errors = result.Errors});

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies)
        {
            try
            {
                var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
                var isPersistent = (useCookies == true) && (useSessionCookies != true);
                _signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;
                
                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, isPersistent, lockoutOnFailure: false);

                if (result.Succeeded) return Ok(new { Message = "Login successful."});
            
                return BadRequest(new { Message = "Login unsucessful. Either the password or username is incorrect."} );
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]   
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok(new { Message = "Logout successful"});

            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut]
        [Route("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return BadRequest(new {Message = "No current user is logged in."});
                
                var result = await _userManager.ChangePasswordAsync(currentUser, 
                    changePasswordDto.CurrentPassword, 
                    changePasswordDto.NewPassword);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Password change sucessful"});
                }
                
                return BadRequest(new {Errors = result.Errors});

            } catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut]
        [Route("current-user/update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(AppUserDto appUserDto)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return BadRequest(new { Message = "No current user is logged in." });

                currentUser.UserName = appUserDto.UserName;
                currentUser.AboutMe = appUserDto.AboutMe;
                currentUser.Email = appUserDto.Email;
                currentUser.FirstName = appUserDto.FirstName;
                currentUser.LastName = appUserDto.LastName;
                currentUser.ProfilePhotoUrl = appUserDto.ProfilePhotoUrl;

                var result = _userManager.UpdateAsync(currentUser);
                if (result.IsCompletedSuccessfully)
                {
                    return Ok(new { Message = "User Update Successful" });
                }

                return BadRequest(new { Errors = result.Result });
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return BadRequest(new{ Message = "There is no current user to delete."});

                var result = await _userManager.DeleteAsync(currentUser);

                if (result.Succeeded)
                    return Ok(new { Message = "Current user deleted sucessfully."});
            
                return BadRequest(new { Errors = result.Errors});
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }

}