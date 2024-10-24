using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolShare.Data.Models;
using ToolShare.Data.Dtos;
using ToolShare.Data.DTOs;

namespace ToolShare.Api.Controllers
{
    //TO DO: Rewrite to convert to using with SQLite Dbase
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Index()
        {
            return "Works";
        }

        [HttpGet]
        [Authorize]
        [Route("users")]
        public async Task<List<AppUserDto>> GetUsers()
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
        [Route("userinfo")]
        [Authorize]
        public async Task<AppUserDto> GetUserInfo()
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

        [HttpPost]   
        [Route("register")]
         public async Task<IActionResult> Register([FromBody] RegistrationDto model)
        {
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Registration successful"});
            }
            return BadRequest(new {Errors = result.Errors});
        }

        [HttpPost]
        [Route("login")]
        public async Task<IResult> Login([FromBody] LoginRequestDto login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies)
        {
            var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
            var isPersistent = (useCookies == true) && (useSessionCookies != true);
            _signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, isPersistent, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            // The signInManager already produced the needed response in the form of a cookie or bearer token.
            return TypedResults.Empty;
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
    }

}