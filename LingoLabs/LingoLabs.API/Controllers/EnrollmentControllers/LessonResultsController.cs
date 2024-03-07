using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.CreateLessonResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.DeleteLessonResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.UpdateLessonResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.EnrollmentControllers
{
    public class LessonResultsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateLessonResultCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdLessonResultQuery(id));

            if (result.LessonResultId == Guid.Empty)
            {
                return NotFound("LessonResult not found.\"");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteLessonResultCommand { LessonResultId = id });
            if (!result.Success)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateLessonResultCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result.UpdateLessonResult);
        }
    }
}
