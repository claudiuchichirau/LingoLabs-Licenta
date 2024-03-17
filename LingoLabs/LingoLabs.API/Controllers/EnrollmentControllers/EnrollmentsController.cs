using LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Commands.CreateEnrollment;
using LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Commands.DeleteEnrollment;
using LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Queries.GetAll;
using LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Queries.GetAllByUserId;
using LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingoLabs.API.Controllers.EnrollmentControllers
{
    public class EnrollmentsController : ApiControllerBase
    {
        [Authorize(Roles = "Admin, Student")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateEnrollmentCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result.ValidationsErrors);
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllEnrollmentsQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Admin, Student")]
        [HttpGet("userEnrollments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByUserId()
        {
            var result = await Mediator.Send(new GetAllEnrollmentsByUserIdQuery());
            return Ok(result);
        }

        [Authorize(Roles = "Admin, Student")]
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

        [Authorize(Roles = "Admin, Student")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteEnrollmentCommand { EnrollmentId = id });

            if (!result.Success)
            {
                return NotFound(result.ValidationsErrors);
            }

            return Ok(result);
        }
    }
}
