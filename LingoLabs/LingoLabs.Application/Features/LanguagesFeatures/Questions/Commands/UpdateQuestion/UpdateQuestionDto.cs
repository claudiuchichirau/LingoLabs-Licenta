using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionDto
    {
        public string QuestionRequirement { get; set; } = string.Empty;
        public LearningType QuestionLearningType { get; set; }
        public byte[] QuestionImageData { get; set; } = [];
        public string QuestionVideoLink { get; set; } = string.Empty;
    }
}
