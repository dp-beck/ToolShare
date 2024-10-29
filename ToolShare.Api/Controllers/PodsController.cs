using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/[controller]")]
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
        [Authorize]
        public async Task<IActionResult> GetAllPods()
        {
            try
            {
                var pods = await _podsRepository.GetAllAsyncWithIncludes(p => p.PodMembers);

                List<PodDto> podDtos = _mapper.Map<List<PodDto>>(pods);
                
                return Ok(podDtos);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost]
        [Authorize(Roles = "NoPodUser")]
        public async Task<IActionResult> InitializeNewPod([FromBody] PodDto podDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = HttpContext.User;
            var appUser = await _userManager.GetUserAsync(user);

            Pod pod = new Pod
            {
                Name = podDto.Name
            };

            pod.PodMembers.Add(appUser);

            await _userManager.AddToRoleAsync(appUser, "PodManager");
            await _userManager.RemoveFromRoleAsync(appUser, "NoPodUser");

            await _podsRepository.CreatePod(pod);
            
            return CreatedAtAction(nameof(InitializeNewPod), new { podId = pod.PodId }, pod);
        }

        [HttpPut]
        [Authorize(Roles = "PodManager")]
        [Route("{podId}")]
        public async Task<IActionResult> AddUserToPod(int podId, [FromBody] string requesterId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = HttpContext.User;
            var currentUser = await _userManager.GetUserAsync(user);

            if (currentUser.PodManagedId != podId)
                return StatusCode(401);

            var pod = await _podsRepository.GetByIdAsync(podId);
            var requester = await _userManager.FindByIdAsync(requesterId);

            if (requester.PodJoined is not null)
                return BadRequest(new {Message = "User already a member of a pod."});
            
            await _userManager.RemoveFromRoleAsync(requester, "NoPodUser");
            await _userManager.AddToRoleAsync(requester, "User");

            await _podsRepository.AddUserToPod(requester, pod);

            return Ok(new { Message = "User added to pod." });
        }
    }
}