using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    [Route("api/users/{username}/[controller]")]
    public class JoinPodRequestsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJoinPodRequestsRepository _joinPodRequestsRepository;

        public JoinPodRequestsController(UserManager<AppUser> userManager,
            IJoinPodRequestsRepository joinPodRequestsRepository)
        {
            _userManager = userManager;
            _joinPodRequestsRepository = joinPodRequestsRepository;
        }

        [HttpPost]
        [Authorize(Roles = "NoPodUser")]
        public async Task<IActionResult> CreateJoinPodRequest(string username, JoinPodRequestDto requestDto)
        {
            var user = await _userManager.FindByNameAsync(username);

            JoinPodRequest request = new JoinPodRequest
            {
                ReceiverId = requestDto.ReceiverId,
                RequesterId = user.Id 
            };
            
            await _joinPodRequestsRepository.CreateAsync(request);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "NoPodUser")]
        public async Task<IActionResult> SendJoinPodRequest()
        {
            throw new NotImplementedException();
        }

    }
}