using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LearningStyles.Commands.CreateLearningStyle
{
    public class CreateLearningStyleDto
    {
        public Guid LearningStyleId { get; set; }
        public string? LearningStyleName { get; set; }
        public LearningType? LearningType { get; set; }
    }
}
