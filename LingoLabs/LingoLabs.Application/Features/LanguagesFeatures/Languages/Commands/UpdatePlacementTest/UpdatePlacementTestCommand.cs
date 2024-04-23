using LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreatePlacementTest;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.UpdatePlacementTest
{
    public class UpdatePlacementTestCommand: IRequest<UpdatePlacementTestCommandResponse>
    {
        public Guid LanguageId { get; set; }
        public List<Guid> QuestionsId { get; set; } = [];
    }
}
