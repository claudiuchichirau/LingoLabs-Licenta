using LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Commands.CreateEnrollment;
using LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Queries.GetAll;
using LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.EnrollmentControllers
{
    public class EnrollmentsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateEnrollmentCommand command)
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
            var result = await Mediator.Send(new GetAllEnrollmentsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetByIdEnrollmentQuery(id));

            if (result.EnrollmentId == Guid.Empty)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
