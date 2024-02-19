namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.CreateLanguageLevelResult
{
    public class CreateLanguageLevelResultDto
    {
        public Guid LanguageLevelResultId { get; set; }
        public Guid? LanguageLevelId { get; set; }
        public Guid? EnrollmentId { get; set; }
    }
}
