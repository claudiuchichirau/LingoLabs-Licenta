using LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.CreateQuestion;
using LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.DeleteQuestion;
using LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.UpdateQuestion;
using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetAll;
using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetAllByLanguageCompetenceId;
using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetAllByLanguageLevelId;
using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.LanguageControllers
{
    public class QuestionsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateQuestionCommand command)
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
            var result = await Mediator.Send(new GetAllQuestionsQuery());
            return Ok(result);
        }

        [HttpGet]
        [Route("language-competence/{languageCompetenceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByLanguageCompetenceId(Guid languageCompetenceId)
        {
            var result = await Mediator.Send(new GetAllQuestionsByLanguageCompetenceIdQuery { LanguageCompetenceId = languageCompetenceId });
            return Ok(result);
        }

        [HttpGet]
        [Route("language-level/{languageLevelId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByLanguageLevelId(Guid languageLevelId)
        {
            var result = await Mediator.Send(new GetAllQuestionsByLanguageLevelIdQuery { LanguageLevelId = languageLevelId });
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdQuestionQuery(id));

            if (result.QuestionId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateQuestionCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.ValidationsErrors);
            }
            return Ok(result.UpdateQuestion);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteQuestionCommand { QuestionId = id });
            if (!result.Success)
            {
                return NotFound(result.ValidationsErrors);     // Question not found
            }
            return Ok(result);
        }
    }
}
