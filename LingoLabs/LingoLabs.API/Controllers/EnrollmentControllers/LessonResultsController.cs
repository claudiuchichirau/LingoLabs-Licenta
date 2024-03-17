using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.CreateLessonResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.DeleteLessonResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.UpdateLessonResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.EnrollmentControllers
{
    public class LessonResultsController : ApiControllerBase
    {
        [Authorize(Roles = "Admin, Student")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateLessonResultCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.ValidationsErrors);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin, Student")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdLessonResultQuery(id));

            if (result.LessonResultId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Student")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteLessonResultCommand { LessonResultId = id });
            if (!result.Success)
            {
                return NotFound(result.ValidationsErrors);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Student")]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateLessonResultCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.ValidationsErrors);
            }
            return Ok(result.UpdateLessonResult);
        }
    }
}
