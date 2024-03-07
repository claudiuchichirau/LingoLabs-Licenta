using LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.CreateChoice;
using LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.DeleteChoice;
using LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.UpdateChoice;
using LingoLabs.Application.Features.LanguagesFeatures.Choices.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.LanguageControllers
{
    public class ChoicesController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateChoiceCommand command)
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
            var result = await Mediator.Send(new GetByIdChoiceQuery(id));

            if (result.ChoiceId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateChoiceCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result.UpdateChoiceDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteChoiceCommand { ChoiceId = id });
            if (!result.Success)
            {
                return NoContent();     // Choice not found
            }
            return Ok(result);
        }
    }
}
