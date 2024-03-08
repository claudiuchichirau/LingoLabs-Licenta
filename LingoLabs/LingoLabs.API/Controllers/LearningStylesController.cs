using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries.GetAll;
using LingoLabs.Application.Features.LearningStyles.Commands.CreateLearningStyle;
using LingoLabs.Application.Features.LearningStyles.Queries.GetAll;
using LingoLabs.Application.Features.LearningStyles.Queries.GetById;
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
                return BadRequest(result.ValidationsErrors);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        { 
            var result = await Mediator.Send(new GetAllLearningStylesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdLearningStyleQuery(id));

            if (result.LearningStyleId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
