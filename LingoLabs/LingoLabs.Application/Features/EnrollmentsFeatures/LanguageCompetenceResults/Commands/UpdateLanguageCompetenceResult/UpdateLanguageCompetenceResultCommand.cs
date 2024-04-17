using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.UpdateLanguageCompetenceResult
{
    public class UpdateLanguageCompetenceResultCommand : IRequest<UpdateLanguageCompetenceResultCommandResponse>
    {
        public Guid LanguageCompetenceResultId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
