﻿using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ApiControllerBase
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IUserService userService;
        public UserInfoController(ICurrentUserService currentUserService, IUserService userService)
        {
            this.currentUserService = currentUserService;
            this.userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userInfo = await userService.GetCurrentUserInfoAsync(currentUserService.UserId);

            if (userInfo.IsSuccess)
            {
                return Ok(userInfo.Value);
            }
            else
            {
                return BadRequest(userInfo.Error);
            }
        }
    }
}
