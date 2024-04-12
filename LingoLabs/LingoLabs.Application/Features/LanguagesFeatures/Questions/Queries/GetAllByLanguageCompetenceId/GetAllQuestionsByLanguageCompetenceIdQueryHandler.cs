using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetAllByLanguageCompetenceId
{
    public class GetAllQuestionsByLanguageCompetenceIdQueryHandler : IRequestHandler<GetAllQuestionsByLanguageCompetenceIdQuery, GetAllQuestionsByLanguageCompetenceIdResponse>
    {
        private readonly IQuestionRepository questionRepository;

        public GetAllQuestionsByLanguageCompetenceIdQueryHandler(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }
        public async Task<GetAllQuestionsByLanguageCompetenceIdResponse> Handle(GetAllQuestionsByLanguageCompetenceIdQuery request, CancellationToken cancellationToken)
        {
            GetAllQuestionsByLanguageCompetenceIdResponse response = new();

            var result = await questionRepository.GetQuestionsByLanguageCompetenceIdAsync(request.LanguageCompetenceId);

            if(result.IsSuccess)
            {
                response.Questions = result.Value.Select(question => new QuestionDto
                {
                    QuestionId = question.QuestionId,
                    QuestionRequirement = question.QuestionRequirement,
                    QuestionType = question.QuestionType
                }).ToList();
            }

            return response;
        }
    }
}
