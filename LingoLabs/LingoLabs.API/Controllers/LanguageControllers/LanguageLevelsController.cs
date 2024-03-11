using LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.CreateLanguageLevel;
using LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.DeleteLanguageLevel;
using LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.UpdateLanguageLevel;
using LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries.GetAll;
using LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.LanguageControllers
{
    public class LanguageLevelsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateLanguageLevelCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.ValidationsErrors);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllLanguageLevelsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdLanguageLevelQuery(id));

            if (result.LanguageLevelId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteLanguageLevelCommand { LanguageLevelId = id });

            if (!result.Success)
            {
                return NotFound(result.ValidationsErrors);
            }

            return Ok(result);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateLanguageLevelCommand command)
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
