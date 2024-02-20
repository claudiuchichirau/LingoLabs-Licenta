using LingoLabs.Application.Features.LearningStyles.Commands.CreateLearningStyle;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers
{
    public class LearningStylesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateLearningStyleCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
