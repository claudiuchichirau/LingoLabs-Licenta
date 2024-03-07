using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.DeleteLanguageCompetenceResult
{
    public class DeleteLanguageCompetenceResultCommand: IRequest<DeleteLanguageCompetenceResultCommandResponse>
    {
        public Guid LanguageCompetenceResultId { get; set; }
    }
}
