using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetAllByLanguageLevelId
{
    public class GetAllQuestionsByLanguageLevelIdQueryHandler : IRequestHandler<GetAllQuestionsByLanguageLevelIdQuery, GetAllQuestionsByLanguageLevelIdResponse>
    {
        private readonly IQuestionRepository questionRepository;

        public GetAllQuestionsByLanguageLevelIdQueryHandler(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }
        public async Task<GetAllQuestionsByLanguageLevelIdResponse> Handle(GetAllQuestionsByLanguageLevelIdQuery request, CancellationToken cancellationToken)
        {
            GetAllQuestionsByLanguageLevelIdResponse response = new();

            var result = await questionRepository.GetQuestionsByLanguageLevelIdAsync(request.LanguageLevelId);

            if(result.IsSuccess)
            {
                response.Questions = result.Value.Select(question => new QuestionDto
                {
                    QuestionId = question.QuestionId,
                    QuestionRequirement = question.QuestionRequirement,
                    QuestionLearningType = question.QuestionLearningType
                }).ToList();
            }

            return response;
        }
    }
}
