using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                var tools = await _toolsRepository.GetAllWithIncludes(
                    t => t.ToolOwner, t => t.ToolBorrower!
                );

                List<ToolDto> toolDtos = _mapper.Map<List<ToolDto>>(tools);

                return Ok(toolDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        [HttpGet]
        [Authorize(Roles ="User,Manager")]
        [Route("tools-by-pod/{podId:int}")]
        public async Task<IActionResult> GetToolsByPod(int podId)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                
                if (currentUser is null) return BadRequest(new { Message = "No current user located."});
                if (currentUser.PodJoinedId != podId) return BadRequest(new { Message = "You are not a member of this pod."});
                
                var allTools = await _toolsRepository.GetAllWithIncludes(
                    t => t.ToolOwner);
                
                var podTools = allTools.AsQueryable().Where(t => t.ToolOwner.PodJoinedId == podId);

                List<ToolDto> podToolDtos = _mapper.Map<List<ToolDto>>(podTools);

                return Ok(podToolDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet]
        [Authorize(Roles = "User,Manager")]
        [Route("tools-by-user-owned/{username}")]
        public async Task<IActionResult> GetToolsOwnedByUser(string username)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return BadRequest(new { Message = "No current user located."});
                
                var requestedUser = await _userManager.FindByNameAsync(username);
                if (requestedUser is null) return BadRequest(new { Message = "We could not find the requested user."});

                if (currentUser.PodJoinedId != requestedUser.PodJoinedId) return BadRequest(new { Message = "You are not a member of the same pod as the requested user."});
                
                var tools = await _toolsRepository.FindToolsOwnedByUsername(username);
                
                List<ToolDto> toolDtos = _mapper.Map<List<ToolDto>>(tools);
                
                return Ok(toolDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet]
        [Authorize(Roles ="User,Manager")]
        [Route("tools-by-user-borrowed/{username}")]
        public async Task<IActionResult> GetToolsBorrowedByUser(string username)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return BadRequest(new { Message = "No current user located."});
                
                var requestedUser = await _userManager.FindByNameAsync(username);
                if (requestedUser is null) return BadRequest(new { Message = "We could not find the requested user."});

                if (currentUser.PodJoinedId != requestedUser.PodJoinedId) return BadRequest(new { Message = "You are not a member of the same pod as the requested user."});

                var tools = await _toolsRepository.FindToolsBorrowedByUsername(username);
                
                List<ToolDto> toolDtos = _mapper.Map<List<ToolDto>>(tools);
                
                return Ok(toolDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        [HttpGet]
        [Authorize]
        [Route("{toolId:int}")]
        public async Task<IActionResult> GetToolById(int toolId)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return BadRequest(new { Message = "No current user located."});
                
                var tool = await _toolsRepository.FindByIdWithIncludes(toolId, t => t.ToolId == toolId,
                    t => t.ToolOwner, t => t.ToolBorrower!);
                
                if (tool is null) return BadRequest(new { Message = "We could not find the requested tool." });
                
                if (currentUser.PodJoinedId != tool.ToolOwner.PodJoinedId) return BadRequest(new { Message = "You are not a member of the same pod as the tool owner."});

                ToolDto toolDto = _mapper.Map<ToolDto>(tool);

                return Ok(toolDto);   
            }
            catch (Exception )
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,PodManager")]
        public async Task<ActionResult<Tool>> CreateTool([FromBody] ToolDto toolDto)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                
                if (currentUser is null) return NotFound("No current user was located");

                Tool tool = new Tool
                {
                    Name = toolDto.Name,
                    Description = toolDto.Description,
                    BorrowingPeriodInDays = toolDto.BorrowingPeriodInDays,
                    ToolPhotoUrl = toolDto.ToolPhotoUrl,
                    ToolOwner = currentUser,
                };
                
                await _toolsRepository.Add(tool);

                return Ok(new { Message = "Tool created successfully."});
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Database Failure: {e.InnerException.Message}");   
                } 
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure {e.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "User,PodManager")]
        [Route("{toolId}")]
        public async Task<IActionResult> UpdateTool(int toolId, [FromBody] UpdateToolDto updateToolDto)
        {
            try
            {
                var oldTool = await _toolsRepository.FindById(toolId);
                if (oldTool == null) return NotFound("Could not find tool with this id.");
                
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return NotFound("Could not find current user.");

                if (oldTool.OwnerId != currentUser.Id)
                    return BadRequest(new {Message = "You are not the owner of this tool."});
            
                _mapper.Map(updateToolDto, oldTool);

                await _toolsRepository.SaveChanges();

                return Ok(new {Message = "Tool Updated Successfully"});
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Database Failure: {e.InnerException.Message}");   
                } 
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure {e.Message}");
            }
        }
        
        [HttpPut]
        [Authorize(Roles = "User,PodManager")]
        [Route("{toolId}/request-tool")]
        public async Task<IActionResult> RequestTool(int toolId)
        {
            try 
            {
                var toolRequested = await _toolsRepository.FindByIdWithIncludes(toolId, t => t.ToolId == toolId, 
                    t => t.ToolOwner);
                if (toolRequested == null) return NotFound("Could not find tool with this id.");
                
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return NotFound("Could not find current user.");
                
                if (toolRequested.ToolOwner.PodJoinedId != currentUser.PodJoinedId)
                    return BadRequest(new { Message = "You are not a member of the same pod as the requested tool owner." });

                if (toolRequested.ToolStatus != ToolStatus.Available)
                    return BadRequest(new { Message = "This tool is not available." });
                
                await _toolsRepository.RequestTool(toolRequested, currentUser);

                return Ok(new {Message = "Tool successfully requested."});
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Database Failure: {e.InnerException.Message}");   
                } 
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure {e.Message}");
            }
        }
        
        [HttpPut]
        [Authorize(Roles = "User,PodManager")]
        [Route("{toolId}/lend-tool")]
        public async Task<IActionResult> LendTool(int toolId)
        {
            try 
            {
                var toolBorrowed = await _toolsRepository.FindByIdWithIncludes(toolId, 
                    t => t.ToolId == toolId,
                    t => t.ToolRequester, t=> t.ToolBorrower);
                if (toolBorrowed == null) return NotFound("Could not find tool with this id.");
                
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return NotFound("Could not find current user.");
                
                if (toolBorrowed.ToolRequester is null) return BadRequest(new {Message = "No one has requested this tool."});

                if (toolBorrowed.OwnerId != currentUser.Id)
                    return BadRequest(new {Message = "You are not the owner of this tool."});
            
                await _toolsRepository.LendTool(toolBorrowed, currentUser);

                return Ok(new {Message = "Tool successfully lent."});
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Database Failure: {e.InnerException.Message}");   
                } 
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure {e.Message}");
            }
        }
        
        [HttpPut]
        [Authorize(Roles = "User,PodManager")]
        [Route("{toolId}/reject-tool-request")]
        public async Task<IActionResult> RejectToolRequest(int toolId)
        {
            try 
            {
                var toolRequested = await _toolsRepository.FindByIdWithIncludes(toolId, 
                    t => t.ToolId == toolId,
                    t => t.ToolRequester, t=> t.ToolBorrower);
                if (toolRequested == null) return NotFound("Could not find tool with this id.");
                
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return NotFound("Could not find current user.");
                
                if (toolRequested.ToolRequester is null) return BadRequest(new {Message = "No one has requested this tool."});

                if (toolRequested.OwnerId != currentUser.Id)
                    return BadRequest(new {Message = "You are not the owner of this tool."});
                
                await _toolsRepository.RejectToolRequest(toolRequested);

                return Ok(new {Message = "Tool request rejected."});
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Database Failure: {e.InnerException.Message}");   
                } 
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure {e.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "User,PodManager")]
        [Route("{toolId}/accept-tool-return")]
        public async Task<IActionResult> AcceptToolReturn(int toolId)
        {
            try 
            {
                var toolReturned = await _toolsRepository.FindByIdWithIncludes(toolId, 
                    t => t.ToolId == toolId,
                    t=> t.ToolBorrower);
                if (toolReturned == null) return NotFound("Could not find tool with this id.");

                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return NotFound("Could not find current user.");

                if (toolReturned.OwnerId != currentUser.Id)
                    return BadRequest(new {Message = "You are not the owner of this tool."});
                
                if (toolReturned.ToolStatus != ToolStatus.ReturnPending)
                    return BadRequest(new {Message = "This tool is not pending return."});
                
                await _toolsRepository.AcceptToolReturn(toolReturned);
                return Ok(new {Message = "Tool return accepted successfully."});
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Database Failure: {e.InnerException.Message}");   
                } 
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure {e.Message}");
            }
        }
        
        [HttpPut]
        [Authorize(Roles = "User,PodManager")]
        [Route("{toolId}/request-tool-return")]
        public async Task<IActionResult> RequestToolReturn(int toolId)
        {
            try 
            {
                var toolBorrowed = await _toolsRepository.FindById(toolId);
                if (toolBorrowed == null) return NotFound("Could not find tool with this id.");

                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return NotFound("Could not find current user.");

                if (toolBorrowed.BorrowerId != currentUser.Id)
                    return BadRequest(new {Message = "You are not the one currently borrowing this tool."});
                
                toolBorrowed.ToolStatus = ToolStatus.ReturnPending;

                await _toolsRepository.SaveChanges();

                return Ok(new {Message = "Tool return request successful."});
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Database Failure: {e.InnerException.Message}");   
                } 
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure {e.Message}");
            }
        }
        
        [HttpDelete]
        [Authorize(Roles = "User,PodManager")]
        [Route("{toolId}")]
        public async Task<ActionResult> DeleteTool(int toolId)
        {
            try
            {
                var tool = await _toolsRepository.FindByIdWithIncludes(toolId, 
                    t => t.ToolId == toolId,
                    t=> t.ToolBorrower);                
                if (tool is null) return NotFound("Could not find tool with this id.");

                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return NotFound("Could not find current user.");

                if (tool.OwnerId != currentUser.Id)
                    return BadRequest(new { Message = "You are not the owner of this tool." });
                
                if (tool.ToolBorrower != null)
                   return BadRequest(new { Message = "You cannot remove a tool that is currently borrowed." }); 

                await _toolsRepository.Delete(tool);

                return Ok(new { Message = "Tool successfully deleted" });
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Database Failure: {e.InnerException.Message}");   
                } 
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure {e.Message}");
            }
        }

    }
}