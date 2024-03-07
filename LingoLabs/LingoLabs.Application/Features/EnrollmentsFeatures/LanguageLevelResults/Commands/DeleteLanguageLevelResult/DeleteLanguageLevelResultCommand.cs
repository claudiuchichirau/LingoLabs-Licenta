using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.DeleteLanguageLevelResult
{
    public class DeleteLanguageLevelResultCommand: IRequest<DeleteLanguageLevelResultCommandResponse>
    {
        public Guid LanguageLevelResultId { get; set; }
    }
}
