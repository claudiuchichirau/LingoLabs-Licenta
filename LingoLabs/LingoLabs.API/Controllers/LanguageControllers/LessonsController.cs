using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateLesson;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries.GetAll;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.LanguageControllers
{
    public class LessonsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateLessonCommand command)
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
            var result = await Mediator.Send(new GetAllLessonsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdLessonQuery(id));

            if (result.LessonId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
