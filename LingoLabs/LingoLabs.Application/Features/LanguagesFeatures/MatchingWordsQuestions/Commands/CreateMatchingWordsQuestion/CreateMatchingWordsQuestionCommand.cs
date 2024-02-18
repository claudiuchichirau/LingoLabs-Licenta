using LingoLabs.Domain.Entities.Languages;
using LingoLabs.Domain.Entities;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Commands.CreateMatchingWordsQuestion
{
    public class CreateMatchingWordsQuestionCommand: IRequest<CreateMatchingWordsQuestionCommandResponse>
    {
        public string QuestionRequirement { get; set; } = default!;
        public LearningType QuestionLearningType { get; set; }
        public List<WordPair> WordPairs { get; private set; } = default!;
    }
}
