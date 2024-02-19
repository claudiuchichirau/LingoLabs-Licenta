using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommand: IRequest<CreateEnrollmentCommandResponse>
    {
        public Guid UserId { get; set; }
        public Guid LanguageId { get; set; }
    }
}
