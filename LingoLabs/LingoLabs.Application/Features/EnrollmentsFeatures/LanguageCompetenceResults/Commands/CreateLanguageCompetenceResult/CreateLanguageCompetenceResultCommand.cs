using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.CreateLanguageCompetenceResult
{
    public class CreateLanguageCompetenceResultCommand: IRequest<CreateLanguageCompetenceResultCommandResponse>
    {
        public Guid LanguageCompetenceId { get; set; }
        public Guid ChapterResultId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
