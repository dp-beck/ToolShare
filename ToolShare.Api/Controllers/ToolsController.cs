using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    [Route("api/tools")]
    public class ToolsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IToolsRepository _toolsRepository;
        private readonly IMapper _mapper;

        public ToolsController( 
            UserManager<AppUser> userManager,
            IToolsRepository toolsRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _toolsRepository = toolsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTools()
        {
            var tools = await _toolsRepository.GetAllAsyncWithIncludes(
                t => t.ToolOwner, t => t.ToolBorrower
            );

            List<ToolDto> toolDtos = _mapper.Map<List<ToolDto>>(tools);

            return Ok(toolDtos);
        }

        [HttpGet]
        [Authorize]
        [Route("{toolId}")]
        public async Task<IActionResult> GetToolById(int toolId)
        {
            try
            {
            var tool = await _toolsRepository.GetByIdAsyncWithIncludes(toolId,
                t => t.ToolOwner, t => t.ToolBorrower);
            
            ToolDto toolDto = _mapper.Map<ToolDto>(tool);

            return Ok(toolDto);   
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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

            return CreatedAtAction(nameof(CreateTool), new { toolId = tool.ToolId }, tool);
        }

    }
}