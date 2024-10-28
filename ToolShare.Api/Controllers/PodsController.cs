using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public PodsController(IPodsRepository podsRepository, 
            UserManager<AppUser> userManager)
        {
            _podsRepository = podsRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllPods()
        {
            return Ok(await _podsRepository.GetAllAsync());
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

            _userManager.AddToRoleAsync(appUser, "PodManager");

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

            _podsRepository.AddUserToPod(requester, pod);

            return Ok(new { Message = "User added to pod." });
        }
    }
}