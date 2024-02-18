using LingoLabs.Domain.Entities;
using MediatR;

namespace LingoLabs.Application.Features.LearningStyles.Commands.CreateLearningStyle
{
    public class CreateLearningStyleCommand: IRequest<CreateLearningStyleCommandResponse>
    {
        public string LearningStyleName { get; set; } = default!;
        public LearningType LearningType { get; set; }
    }
}
