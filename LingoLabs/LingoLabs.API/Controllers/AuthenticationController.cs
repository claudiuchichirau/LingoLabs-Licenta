using LingoLabs.API.Models;
using LingoLabs.Application.Contracts.Identity;
using LingoLabs.Application.Contracts.Interfaces;
using LingoLabs.Application.Models.Identity;
using LingoLabs.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly GetRegistrationStrategy _registrationStrategy;
        private readonly ICurrentUserService currentUserService;

        public AuthenticationController(ILoginService authService, ILogger<AuthenticationController> logger, GetRegistrationStrategy registrationStrategy, ICurrentUserService currentUserService)
        {
            _loginService = authService;
            _logger = logger;
            _registrationStrategy = registrationStrategy;
            this.currentUserService = currentUserService;
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
        [Route("student-register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> StudentRegister(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                IRegistrationServiceStrategy registrationStrategy = _registrationStrategy.GetRegistrationRoleStrategy("Student");
                var (status, message) = await registrationStrategy.Registration(model);

                if (status == UserAuthenticationStatus.REGISTRATION_FAIL)
                {
                    return BadRequest(message);
                }


                return CreatedAtAction(nameof(StudentRegister), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("admin-register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdminRegister(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                IRegistrationServiceStrategy registrationStrategy = _registrationStrategy.GetRegistrationRoleStrategy("Admin");
                var (status, message) = await registrationStrategy.Registration(model);

                if (status == UserAuthenticationStatus.REGISTRATION_FAIL)
                {
                    return BadRequest(message);
                }


                return CreatedAtAction(nameof(AdminRegister), model);
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
            if (this.currentUserService.GetCurrentUserId() == null)
            {
                return new CurrentUser
                {
                    IsAuthenticated = false
                };
            }
            return new CurrentUser
            {
                IsAuthenticated = true,
                UserName = this.currentUserService.GetCurrentUserId(),
                Claims = this.currentUserService.GetCurrentClaimsPrincipal().Claims.ToDictionary(c => c.Type, c => c.Value)
            };
        }
    }
}
