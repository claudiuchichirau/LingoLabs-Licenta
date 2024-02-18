using LingoLabs.Domain.Entities;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Commands.CreateMatchingWordsQuestion
{
    public class CreateMatchingWordsQuestionDto
    {
        public Guid QuestionId { get; set; }
        public string? QuestionRequirement { get; set; }
        public LearningType? QuestionLearningType { get; set; }
        public List<WordPair>? WordPairs { get; set; }
    }
}
