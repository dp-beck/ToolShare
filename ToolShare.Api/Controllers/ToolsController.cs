using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolShare.Api.Dtos;
using ToolShare.Data;
using ToolShare.Data.Models;
using ToolShare.Data.Repositories;

//TO DO: Rewrite to use Repository Architecture -- Written this way just to test things 
namespace ToolShare.Api.Controllers
{
    [ApiController]
    [Route("api/users/{username}/[controller]")]
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
            return Ok(await _toolsRepository.GetAllTools());
        }

        [HttpPost]
        public async Task<ActionResult<Tool>> CreateTool(string username, [FromBody] ToolDto toolDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("User not found");

            Tool tool = new Tool
            {
                Name = toolDto.Name,
                Description = toolDto.Description,
                BorrowingPeriodInDays = toolDto.BorrowingPeriodInDays,  
            };

            tool.ToolOwner = user;

            _context.Tools.Add(tool);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateTool), new { toolId = tool.ToolId }, tool);
        }

    }
}