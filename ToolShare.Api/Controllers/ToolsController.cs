using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolShare.Api.Dtos;
using ToolShare.Data;
using ToolShare.Data.Models;
using ToolShare.Data.Repositories;

namespace ToolShare.Api.Controllers
{
    [ApiController]
    [Route("api/users/currentuser/tools")]
    public class ToolsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IToolsRepository _toolsRepository;

        public ToolsController(ApplicationDbContext context, 
            UserManager<AppUser> userManager,
            IToolsRepository toolsRepository)
        {
            _context = context;
            _userManager = userManager;
            _toolsRepository = toolsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTools()
        {
            return Ok(await _toolsRepository.GetAllAsyncWithIncludes());
        }

        [HttpPost]
        [Authorize(Roles = "User,PodManager")]
        public async Task<ActionResult<Tool>> CreateTool([FromBody] ToolDto toolDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = HttpContext.User;
            var appUser = await _userManager.GetUserAsync(user);

            Tool tool = new Tool
            {
                Name = toolDto.Name,
                Description = toolDto.Description,
                BorrowingPeriodInDays = toolDto.BorrowingPeriodInDays,  
            };

            tool.ToolOwner = appUser;

            await _toolsRepository.AddAsync(tool);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateTool), new { toolId = tool.ToolId }, tool);
        }

    }
}