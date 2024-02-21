using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Queries.GetById
{
    public class GetByIdMatchingQuestionQueryHandler : IRequestHandler<GetByIdMatchingQuestionQuery, MatchingWordsQuestionDto>
    {
        private readonly IMatchingWordsQuestionRepository repository;

        public GetByIdMatchingQuestionQueryHandler(IMatchingWordsQuestionRepository repository)
        {
            this.repository = repository;
        }
        public async Task<MatchingWordsQuestionDto> Handle(GetByIdMatchingQuestionQuery request, CancellationToken cancellationToken)
        {
            var matchingQuestion = await repository.FindByIdAsync(request.Id);
            if(matchingQuestion.IsSuccess)
            {
                return new GetSingleMatchingQuestionDto
                {
                    QuestionId = matchingQuestion.Value.QuestionId,
                    QuestionRequirement = matchingQuestion.Value.QuestionRequirement,
                    QuestionLearningType = matchingQuestion.Value.QuestionLearningType,

                    WordPairs = matchingQuestion.Value.WordPairs.Select(wordPair => new WordPairs.Queries.WordPairDto
                    {
                        WordPairId = wordPair.WordPairId,
                        KeyWord = wordPair.KeyWord,
                        ValueWord = wordPair.ValueWord
                    }).ToList(),

                    QuestionImageData = matchingQuestion.Value.QuestionImageData,
                    QuestionVideoLink = matchingQuestion.Value.QuestionVideoLink,
                    LessonId = matchingQuestion.Value.LessonId
                };
            }

            return new GetSingleMatchingQuestionDto();
        }
    }
}
