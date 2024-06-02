using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateLesson;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.DeleteLesson;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.DeleteQuiz;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateLesson;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateQuiz;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries.GetAll;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.LanguageControllers
{
    public class LessonsController : ApiControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateLessonCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.ValidationsErrors);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-quiz")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateQuiz(CreateQuizCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.ValidationsErrors);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-quiz/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            var result = await Mediator.Send(new DeleteQuizCommand { LessonId = id });

            if (!result.Success)
            {
                return NotFound(result.ValidationsErrors);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]   
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllLessonsQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Admin, Student, AdminPending")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdLessonQuery(id));

            //string content = result.LessonQuestions[0].Choices[0].ChoiceContent;

            if (result.LessonId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteLessonCommand { LessonId = id });

            if (!result.Success)
            {
                return NotFound(result.ValidationsErrors);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateLessonCommand command)
        {
            var lessonRequirement = command.LessonRequirement;
            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return NotFound(result.ValidationsErrors);
            }

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-quiz")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateQuiz(UpdateQuizCommand command)
        {
            var result = await Mediator.Send(command);

            if (!result.Success)
            {
                return NotFound(result.ValidationsErrors);
            }

            return Ok(result);
        }
    }
}
