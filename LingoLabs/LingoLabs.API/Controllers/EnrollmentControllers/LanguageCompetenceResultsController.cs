using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.CreateLanguageCompetenceResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.DeleteLanguageCompetenceResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.UpdateLanguageCompetenceResult;
using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.EnrollmentControllers
{
    public class LanguageCompetenceResultsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateLanguageCompetenceResultCommand command)
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
            var result = await Mediator.Send(new GetByIdLanguageCompetenceResultQuery(id));

            if (result.LanguageCompetenceResultId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteLanguageCompetenceResultCommand { LanguageCompetenceResultId = id });

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateLanguageCompetenceResultCommand command)
        {
            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
