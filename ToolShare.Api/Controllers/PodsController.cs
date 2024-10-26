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

        [HttpPost]
        [Authorize]
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
            await _podsRepository.CreatePod(pod);
            
            return CreatedAtAction(nameof(InitializeNewPod), new { podId = pod.PodId }, pod);

        }
    }
}