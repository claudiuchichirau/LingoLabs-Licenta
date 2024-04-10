using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetAll
{
    public class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, GetAllQuestionsResponse>
    {
        private readonly IQuestionRepository repository;

        public GetAllQuestionsQueryHandler(IQuestionRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetAllQuestionsResponse> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            GetAllQuestionsResponse response = new();
            var result = await repository.GetAllAsync();
            if(result.IsSuccess)
            {
                response.Questions = result.Value.Select(x => new QuestionDto
                {
                    QuestionId = x.QuestionId,
                    QuestionRequirement = x.QuestionRequirement,
                    QuestionLearningType = x.QuestionLearningType,
                    QuestionPriorityNumber = x.QuestionPriorityNumber,
                    LessonId = x.LessonId
                }).ToList();
            }

            return response;
        }
    }
}
