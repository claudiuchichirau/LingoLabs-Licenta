using LingoLabs.Domain.Entities;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateQuiz
{
    public class UpdateQuestionForDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionRequirement { get; set; } = string.Empty;
        public LearningType QuestionLearningType { get; set; }
        public byte[] QuestionImageData { get; set; } = [];
        public string QuestionVideoLink { get; set; } = string.Empty;
        public List<UpdateChoiceForDto> Choices { get; set; } = [];
    }
}
