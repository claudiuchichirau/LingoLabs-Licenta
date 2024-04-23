using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreatePlacementTest
{
    public class CreatePlacementTestCommand: IRequest<CreatePlacementTestCommandResponse>
    {
        public Guid LanguageId { get; set; }
        public List<Guid> QuestionsId { get; set; } = [];
    }
}
