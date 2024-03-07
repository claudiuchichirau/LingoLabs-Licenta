using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.UpdateLanguageCompetenceResult
{
    public class UpdateLanguageCompetenceResultCommand : IRequest<UpdateLanguageCompetenceResultCommandResponse>
    {
        public Guid LanguageCompetenceResultId { get; set; }
        public UpdateLanguageCompetenceResultDto UpdateLanguageCompetenceResultDto { get; set; } = new UpdateLanguageCompetenceResultDto();
    }
}
