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
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> GetAllTools()
        {
            try
            {
                var tools = await _toolsRepository.GetAllAsyncWithIncludes(
                    t => t.ToolOwner, t => t.ToolBorrower!
                );

                List<ToolDto> toolDtos = _mapper.Map<List<ToolDto>>(tools);

                return Ok(toolDtos);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("{toolId}")]
        public async Task<IActionResult> GetToolById(int toolId)
        {
            try
            {
            var tool = await _toolsRepository.GetByIdAsyncWithIncludes(toolId, t => t.ToolId == toolId,
                t => t.ToolOwner, t => t.ToolBorrower!);
            
            ToolDto toolDto = _mapper.Map<ToolDto>(tool);

            return Ok(toolDto);   
            }
            catch (Exception )
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,PodManager")]
        public async Task<ActionResult<Tool>> CreateTool([FromBody] ToolDto toolDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                
                if (currentUser is null) return BadRequest(new { Message = "No current user is logged in."});

                Tool tool = new Tool
                {
                    Name = toolDto.Name,
                    Description = toolDto.Description,
                    BorrowingPeriodInDays = toolDto.BorrowingPeriodInDays,  
                };
                
                tool.ToolOwner = currentUser;
                await _toolsRepository.AddAsync(tool);

                return Ok(new { Message = "Tool created successfully."});
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut]
        [Authorize(Roles = "User,PodManager")]
        [Route("{toolId}")]
        public async Task<IActionResult> UpdateTool(int toolId, [FromBody] UpdateToolDto updateToolDto)
        {
            try
            {
                var oldTool = await _toolsRepository.GetByIdAsync(toolId);
                if (oldTool == null) return NotFound("Could not find tool with this id.");
                
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return NotFound("Could not find current user.");

                if (oldTool.OwnerId != currentUser.Id)
                return BadRequest(new {Message = "You are not the owner of this tool."});
            
                _mapper.Map(updateToolDto, oldTool);

                await _toolsRepository.SaveChangesAsync();

                return Ok(new {Message = "Tool Updated Successfully"});
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut]
        [Authorize(Roles = "User,PodManager")]
        [Route("{toolId}/lendtool")]
        public async Task<IActionResult> LendTool(int toolId, [FromBody] string toolBorrowerUserName)
        {
            try 
            {
                var toolBorrowed = await _toolsRepository.GetByIdAsync(toolId);
                if (toolBorrowed == null) return NotFound("Could not find tool with this id.");

                var ToolBorrower = await _userManager.FindByNameAsync(toolBorrowerUserName);
                if (ToolBorrower == null) return NotFound("Could not find tool with this id.");

                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return NotFound("Could not find current user.");

                if (toolBorrowed.OwnerId != currentUser.Id)
                    return BadRequest(new {Message = "You are not the owner of this tool."});
            
                toolBorrowed.ToolBorrower = ToolBorrower;
                toolBorrowed.ToolStatus = ToolStatus.CurrentlyBorrowed;

                await _toolsRepository.SaveChangesAsync();

                return Ok(new {Message = "Tool successfully lent."});
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut]
        [Authorize(Roles = "User,PodManager")]
        [Route("{toolId}/returntool")]
        public async Task<IActionResult> ReturnTool(int toolId)
        {
            try 
            {
                var toolBorrowed = await _toolsRepository.GetByIdAsync(toolId);
                if (toolBorrowed == null) return NotFound("Could not find tool with this id.");

                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return NotFound("Could not find current user.");

                if (toolBorrowed.OwnerId != currentUser.Id)
                return BadRequest(new {Message = "You are not the owner of this tool."});
            
                toolBorrowed.ToolBorrower = null;
                toolBorrowed.BorrowerId = null;
                toolBorrowed.ToolStatus = ToolStatus.AvailableForBorrowing;

                await _toolsRepository.SaveChangesAsync();

                return Ok(new {Message = "Tool successfully returned."});
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "User,PodManager")]
        [Route("{toolId}")]
        public async Task<ActionResult> DeleteTool(int toolId)
        {
            try
            {
                var tool = await _toolsRepository.GetByIdAsync(toolId);
                if (tool is null) return NotFound("Could not find tool with this id.");

                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return NotFound("Could not find current user.");

                if (tool.OwnerId != currentUser.Id)
                    return BadRequest(new { Message = "You are not the owner of this tool." });

                await _toolsRepository.DeleteAsync(tool);

                return Ok(new { Message = "Tool successfully deleted" });
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

    }
}