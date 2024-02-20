using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries.GetAll;
using LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Commands.CreateWordPair;
using LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Queries.GetAll;
using LingoLabs.Application.Features.LanguagesFeatures.WordPairs.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.LanguageControllers
{
    public class WordPairsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateWordPairCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllWordPairsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdWordPairQuery(id));

            if (result.WordPairId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
