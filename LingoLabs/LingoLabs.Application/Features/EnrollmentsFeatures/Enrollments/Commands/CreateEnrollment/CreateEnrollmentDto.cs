namespace LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Commands.CreateEnrollment
{
    public class CreateEnrollmentDto
    {
        public Guid EnrollmentId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? LanguageId { get; set; }
    }
}
