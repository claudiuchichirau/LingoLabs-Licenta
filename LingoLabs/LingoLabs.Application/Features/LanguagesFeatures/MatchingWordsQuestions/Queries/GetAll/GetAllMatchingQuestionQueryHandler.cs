using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.MatchingWordsQuestions.Queries.GetAll
{
    public class GetAllMatchingQuestionQueryHandler : IRequestHandler<GetAllMatchingQuestionQuery, GetAllMatchingQuestionResponse>
    {
        private readonly IMatchingWordsQuestionRepository repository;

        public GetAllMatchingQuestionQueryHandler(IMatchingWordsQuestionRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetAllMatchingQuestionResponse> Handle(GetAllMatchingQuestionQuery request, CancellationToken cancellationToken)
        {
            GetAllMatchingQuestionResponse response = new();
            var result = await repository.GetAllAsync();
            if(result.IsSuccess)
            {
                response.MatchingWordsQuestions = result.Value.Select(matchingQuestion => new MatchingWordsQuestionDto
                {
                    QuestionId = matchingQuestion.QuestionId,
                    QuestionRequirement = matchingQuestion.QuestionRequirement,
                    QuestionLearningType = matchingQuestion.QuestionLearningType
                }).ToList();
            }

            return response;
        }
    }
}
