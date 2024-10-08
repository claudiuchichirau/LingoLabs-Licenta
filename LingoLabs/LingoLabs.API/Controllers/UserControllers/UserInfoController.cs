﻿using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Application.Contracts.Interfaces;
using LingoLabs.Application.Models.Identity;
using LingoLabs.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.UserControllers
{
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("pendingadmins")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPendingAdmins()
        {
            var pendingAdmins = await userService.GetPendingAdmins();

            if(pendingAdmins.IsSuccess)
            {
                return Ok(pendingAdmins.Value);
            }
            else
            {
                return BadRequest(pendingAdmins.Error);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("approveadmin/{userId}")]
        public async Task<IActionResult> ApproveAdmin(string userId)
        {
            var (status, message) = await userService.ApproveAdmin(userId);

            if (status == UserAuthenticationStatus.REGISTRATION_SUCCES)
            {
                return Ok(message);
            }
            else
            {
                return BadRequest(message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("rejectadmin/{userId}")]
        public async Task<IActionResult> RejectAdmin(string userId)
        {
            var (status, message) = await userService.RejectAdmin(userId);

            if (status == UserAuthenticationStatus.REGISTRATION_SUCCES)
            {
                return Ok(message);
            }
            else
            {
                return BadRequest(message);
            }
        }

        [Authorize]
        [HttpDelete("deletecurrentuser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            var result = await userService.DeleteCurrentUser(currentUserService.UserId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        [Authorize(Roles = "Admin, Student, AdminPending")]
        [HttpPut("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var result = await userService.ChangePassword(currentUserService.UserId, model);

            if (result.success)
            {
                return Ok(result.message);
            }
            else
            {
                return BadRequest(result.message);
            }
        }

        [Authorize(Roles = "Admin, Student, AdminPending")]
        [HttpPut("update-user-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserInfo(UserInfoModel model)
        {
            var result = await userService.UpdateUserInfoAsync(currentUserService.UserId, model);

            if (result.success)
            {
                return Ok(result.message);
            }
            else
            {
                return BadRequest(result.message);
            }
        }
    }
}
