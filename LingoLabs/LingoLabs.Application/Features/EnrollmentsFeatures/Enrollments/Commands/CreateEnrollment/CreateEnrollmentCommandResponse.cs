using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentCommandResponse: BaseResponse
    {
        public CreateEnrollmentCommandResponse() : base()
        {
        }

        public CreateEnrollmentDto Enrollment { get; set; }
    }
}
