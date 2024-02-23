using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LearningStyles.Queries
{
    public class LearningStyleDto
    {
        public Guid LearningStyleId { get; set; }
        public string LearningStyleName { get; set; } = string.Empty;
        public LearningType LearningType { get; set; }
    }
}
