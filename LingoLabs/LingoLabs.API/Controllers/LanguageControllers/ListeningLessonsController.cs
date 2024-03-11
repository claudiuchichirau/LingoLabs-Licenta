using LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.CreateListeningLesson;
using LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.UpdateListeningLesson;
using LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Queries.GetAll;
using LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.LanguageControllers
{
    public class ListeningLessonsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateListeningLessonCommand command)
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
            var result = await Mediator.Send(new GetAllListeningLessonsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdListeningLessonQuery(id));

            if (result.LessonId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateListeningLessonCommand command)
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
