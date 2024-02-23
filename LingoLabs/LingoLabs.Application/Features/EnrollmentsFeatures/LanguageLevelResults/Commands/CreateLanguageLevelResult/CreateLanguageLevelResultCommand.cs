using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.CreateLanguageLevelResult
{
    public class CreateLanguageLevelResultCommand: IRequest<CreateLanguageLevelResultCommandResponse>
    {
        public Guid LanguageLevelId { get; set; }
        public Guid EnrollmentId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
