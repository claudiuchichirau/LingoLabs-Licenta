using LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.CreateChapterResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.DeleteChapterResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.UpdateChapterResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.EnrollmentControllers
{
    public class ChapterResultsController : ApiControllerBase
    {
        [Authorize(Roles = "Admin, Student")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateChapterResultCommand command)
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
            var result = await Mediator.Send(new GetByIdChapterResultQuery(id));

            if (result.ChapterResultId == Guid.Empty)
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
            var result = await Mediator.Send(new DeleteChapterResultCommand { ChapterResultId = id });

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
        public async Task<IActionResult> Update(UpdateChapterResultCommand command)
        {
            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result.ValidationsErrors);
            }

            return Ok(result);
        }
    }
}
