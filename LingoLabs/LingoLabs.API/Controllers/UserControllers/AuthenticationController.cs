using LingoLabs.API.Models;
using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Application.Contracts.Interfaces;
using LingoLabs.Application.Models.Identity;
using LingoLabs.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.UserControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly GetRegistrationStrategy _registrationStrategy;
        private readonly ICurrentUserService currentUserService;
        private readonly IUserService userService;

        public AuthenticationController(ILoginService authService, ILogger<AuthenticationController> logger, GetRegistrationStrategy registrationStrategy, ICurrentUserService currentUserService, IUserService userService)
        {
            _loginService = authService;
            _logger = logger;
            _registrationStrategy = registrationStrategy;
            this.currentUserService = currentUserService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _loginService.Login(model);

                if (status == UserAuthenticationStatus.LOGIN_FAIL)
                {
                    return BadRequest(message);
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                IRegistrationServiceStrategy registrationStrategy = _registrationStrategy.GetRegistrationRoleStrategy(model.Role);
                var (status, message) = await registrationStrategy.Registration(model);

                if (status == UserAuthenticationStatus.REGISTRATION_FAIL)
                {
                    return BadRequest(message);
                }


                return CreatedAtAction(nameof(Register), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _loginService.Logout();
            return Ok();
        }

        [HttpGet]
        [Route("currentuserinfo")]
        public CurrentUser CurrentUserInfo()
        {
            if (currentUserService.GetCurrentUserId() == null)
            {
                return new CurrentUser
                {
                    IsAuthenticated = false
                };
            }
            return new CurrentUser
            {
                IsAuthenticated = true,
                UserId = currentUserService.GetCurrentUserId(),
                Claims = currentUserService.GetCurrentClaimsPrincipal().Claims.ToDictionary(c => c.Type, c => c.Value)
            };
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
    }
}
