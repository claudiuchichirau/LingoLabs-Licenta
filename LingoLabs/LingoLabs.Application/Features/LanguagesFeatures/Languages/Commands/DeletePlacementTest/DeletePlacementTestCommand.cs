using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.DeletePlacementTest
{
    public class DeletePlacementTestCommand: IRequest<DeletePlacementTestCommandResponse>
    {
        public Guid LanguageId { get; set; }
    }
}
