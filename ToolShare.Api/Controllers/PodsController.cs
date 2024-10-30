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
            pod.podManager = appUser;

            await _userManager.AddToRoleAsync(appUser, "PodManager");
            await _userManager.RemoveFromRoleAsync(appUser, "NoPodUser");

            await _podsRepository.CreatePod(pod);
            
            return CreatedAtAction(nameof(InitializeNewPod), new { podId = pod.PodId }, pod);
        }

        [HttpPut]
        [Authorize(Roles = "PodManager")]
        [Route("{podId}")]
        public async Task<IActionResult> AddUserToPod([FromBody] string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = HttpContext.User;
            var currentPodManager = await _userManager.GetUserAsync(user);

            if (currentPodManager.PodManagedId == null)   
                return BadRequest(new { Message = "You are not a manager of your pod."});

            var pod = await _podsRepository.GetByIdAsync((int)currentPodManager.PodManagedId);
            var userToAdd = await _userManager.FindByNameAsync(username);

            if (userToAdd.PodJoined is not null)
                return BadRequest(new {Message = "User already a member of a pod."});
            
            await _userManager.RemoveFromRoleAsync(userToAdd, "NoPodUser");
            await _userManager.AddToRoleAsync(userToAdd, "User");

            await _podsRepository.AddUserToPod(userToAdd, pod);

            return Ok(new { Message = "User added to pod." });
        }
    }
}