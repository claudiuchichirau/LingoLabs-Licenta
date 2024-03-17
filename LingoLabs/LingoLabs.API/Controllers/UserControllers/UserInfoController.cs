using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Application.Contracts.Interfaces;
using LingoLabs.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LingoLabs.API.Controllers.UserControllers
{
    [Route("api/v1/[controller]")]
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
    }
}
