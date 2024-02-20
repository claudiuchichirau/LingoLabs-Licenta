using LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Commands.CreateMatchingWordsQuestion;
using LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Queries.GetAll;
using LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.LanguageControllers
{
    public class MatchingWordsQuestionsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateMatchingWordsQuestionCommand command)
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
            var result = await Mediator.Send(new GetAllMatchingQuestionQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdMatchingQuestionQuery(id));

            if (result.QuestionId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
