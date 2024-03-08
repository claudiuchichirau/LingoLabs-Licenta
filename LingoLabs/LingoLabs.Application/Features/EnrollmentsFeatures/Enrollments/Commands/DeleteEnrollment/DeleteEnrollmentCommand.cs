using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Commands.DeleteEnrollment
{
    public class DeleteEnrollmentCommand: IRequest<DeleteEnrollmentCommandResponse>
    {
        public Guid EnrollmentId { get; set; }
    }
}
