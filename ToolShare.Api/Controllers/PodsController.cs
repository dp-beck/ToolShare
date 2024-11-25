using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ToolShare.Api.Dtos;
using ToolShare.Data.Models;
using ToolShare.Data.Repositories;

namespace ToolShare.Api.Controllers
{
    [ApiController]
    [Route("api/pods")]
    public class PodsController : ControllerBase
    {
        private readonly IPodsRepository _podsRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public PodsController(IPodsRepository podsRepository, 
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _podsRepository = podsRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> GetAllPods()
        {
            try
            {
                var pods = await _podsRepository.GetAllAsyncWithIncludes(p => p.PodMembers);

                List<PodDto> podDtos = _mapper.Map<List<PodDto>>(pods);
                
                return Ok(podDtos);
            } 
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpGet] 
        [Authorize(Roles ="NoPodUser")]
        [Route("pod-list-for-nopoduser")]
        public async Task<IActionResult> GetAllPodsLimitedInfoForNoPodUser()
        {
            try
            {
                var pods = await _podsRepository.GetAllAsyncWithIncludes(p => p.podManager);

                var limitedPodDTOs = _mapper.Map<List<LimitedPodInfoDTO>>(pods);
                
                return Ok(limitedPodDTOs);
            } 
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet]
        [Authorize(Roles = "User,PodManager")]
        [Route("{podId:int}")]
        public async Task<IActionResult> FindPodById(int podId)
        {
            try
            {
                var pod = await _podsRepository.GetByIdAsyncWithIncludes(podId, 
                    p => p.PodId == podId, 
                    p => p.PodMembers,
                    p => p.podManager);
                
                if (pod is null) return BadRequest(new {Message = "No pod with that Id exists."});

                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return BadRequest(new { Message = "No current user is logged in."});

                if (pod.PodId != currentUser.PodJoinedId) 
                    return BadRequest(new {Message = "You are not a member of this pod."});

                PodDto podDto = _mapper.Map<PodDto>(pod);
                return Ok(podDto);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet]
        [Authorize(Roles = "User,PodManager")]
        [Route("{podName}")]
        public async Task<IActionResult> FindPodInfoByPodName(string podName)
        {
            try
            {
                var pod = await _podsRepository.FindPodByName(podName);
                if (pod is null) return BadRequest(new {Message = "No pod with that name exists."});

                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return BadRequest(new { Message = "No current user is logged in."});

                if (pod.PodId != currentUser.PodJoinedId)
                    return BadRequest(new {Message = "You are not a member of this pod."});

                PodDto podDto = _mapper.Map<PodDto>(pod);
                return Ok(podDto);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        [Authorize(Roles = "NoPodUser")]
        public async Task<IActionResult> InitializeNewPod([FromBody] PodDto podDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
            
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (currentUser is null) return BadRequest(new { Message = "No current user is logged in."});

                Pod pod = new Pod
                {
                    Name = podDto.Name
                };

                pod.PodMembers.Add(currentUser);
                pod.podManager = currentUser;

                await _userManager.AddToRoleAsync(currentUser, "PodManager");
                await _userManager.AddToRoleAsync(currentUser, "User");
                await _userManager.RemoveFromRoleAsync(currentUser, "NoPodUser");

                await _podsRepository.AddAsync(pod);
            
                return Ok(new { Message = "Pod created successfully."});
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut]
        [Authorize(Roles = "PodManager")]
        [Route("{podId}/add-user")]
        public async Task<IActionResult> AddUserToPod(int podId, [FromBody] string username)
        {
            try
            {
                var currentPodManager = await _userManager.GetUserAsync(HttpContext.User);
                if (currentPodManager is null) return BadRequest(new { Message = "No current user is logged in." });

                if (currentPodManager.PodManagedId != podId) return BadRequest(new { Message = "You are not a manager of this pod." });

                var pod = await _podsRepository.GetByIdAsync(podId);
                if (pod is null) return BadRequest(new { Message = "No pod found" });

                var userToAdd = await _userManager.FindByNameAsync(username);
                if (userToAdd is null) return BadRequest(new { Message = "The user to add was not found." });

                if (userToAdd.PodJoined is not null) return BadRequest(new { Message = "User already a member of a pod." });

                await _userManager.RemoveFromRoleAsync(userToAdd, "NoPodUser");
                await _userManager.AddToRoleAsync(userToAdd, "User");

                await _podsRepository.AddUserToPod(userToAdd, pod);

                return Ok(new { Message = "User added to pod." });
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut]
        [Authorize(Roles = "PodManager")]
        [Route("{podId}/remove-user")]
        public async Task<IActionResult> RemoveUserFromPod(int podId, [FromBody] string username)
        {
            try
            {
                var currentPodManager = await _userManager.GetUserAsync(HttpContext.User);
                if (currentPodManager is null) return BadRequest(new { Message = "No current user is logged in." });

                if (currentPodManager.PodManagedId != podId)   
                    return BadRequest(new { Message = "You are not a manager of your pod."});

                var pod = await _podsRepository.GetByIdAsync(podId);
                if (pod is null) return NotFound("Pod not found");
                
                var userToRemove = await _userManager.FindByNameAsync(username);
                if (userToRemove is null) return BadRequest(new { Message = "This user does not exist."});

                if (userToRemove.PodJoinedId != currentPodManager.PodManagedId)
                    return BadRequest(new {Message = "This user is not a member of your pod"});
            
                if (pod.podManager.Id != userToRemove.Id)
                {
                    await _userManager.RemoveFromRoleAsync(userToRemove, "User");
                    await _userManager.AddToRoleAsync(userToRemove, "NoPodUser");
                }

                await _podsRepository.RemoveUserFromPod(userToRemove, pod);

                return Ok(new { Message = "User removed from pod." });
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }   
        }

        [HttpPut]
        [Authorize(Roles = "PodManager")]
        [Route("{podId}/updatename")]
        public async Task<IActionResult> UpdatePodName(int podId, [FromBody] string newName)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                
                var currentPodManager = await _userManager.GetUserAsync(HttpContext.User);
                if (currentPodManager is null) return BadRequest(new { Message = "No current user is logged in." });

                if (currentPodManager.PodManagedId != podId)   
                    return BadRequest(new { Message = "You are not a manager of your pod."});

                var pod = await _podsRepository.GetByIdAsync(podId);

                if (pod is null) return BadRequest(new {Message ="No pod located with that id"});

                await _podsRepository.UpdateName(newName, pod);
                return Ok(new {Message = "Pod name changed."});
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");               
            }
        }
        
        [HttpPut]
        [Authorize(Roles = "PodManager")]
        [Route("{podId}/change-pod-manager")]
        public async Task<IActionResult> ChangePodManager(int podId, [FromBody] string username)
        {
            try
            {
                var currentPodManager = await _userManager.GetUserAsync(HttpContext.User);
                if (currentPodManager is null) return BadRequest(new { Message = "No current user is logged in." });

                if (currentPodManager.PodManagedId != podId)   
                    return BadRequest(new { Message = "You are not a manager of your pod."});

                var pod = await _podsRepository.GetByIdAsync(podId);
                if (pod is null) return BadRequest(new {Message ="No pod located with that id"});
                
                var newPodManager = await _userManager.FindByNameAsync(username);
                if (newPodManager is null) return BadRequest(new { Message = "This user does not exist."});
                if (newPodManager.PodJoinedId != podId) return BadRequest(new { Message = "This user is not a member of your pod."});
                
                await _userManager.RemoveFromRoleAsync(currentPodManager, "PodManager");
                await _userManager.AddToRoleAsync(newPodManager, "PodManager");
                
                await _podsRepository.ChangeManager(newPodManager, pod);
                return Ok(new {Message = "Pod manager changed."});
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");               
            }
        }

        [HttpDelete]
        [Authorize(Roles = "PodManager")]
        [Route("{podId}")]
        public async Task<IActionResult> DeletePod(int podId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var currentPodManager = await _userManager.GetUserAsync(HttpContext.User);
                if (currentPodManager is null) return BadRequest(new { Message = "No current user is logged in." });
                
                if (currentPodManager.PodManagedId != podId)   
                    return BadRequest(new { Message = "You are not a manager of your pod."});
            
                var pod = await _podsRepository.GetByIdAsync(podId);
                if (pod is null) return BadRequest(new {Message ="No pod located with that id"});

                if (pod.PodMembers.Any())
                    return BadRequest(new { Message = "You must remove all members from the pod before deleting, including yourself"});
                
                await _userManager.RemoveFromRoleAsync(currentPodManager, "PodManager");
                await _userManager.AddToRoleAsync(currentPodManager, "NoPodUser");
            
                await _podsRepository.DeleteAsync(pod);

                return Ok(new {Message = "Pod sucessfully deleted."});
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");               
            }
             
        }
    }
}