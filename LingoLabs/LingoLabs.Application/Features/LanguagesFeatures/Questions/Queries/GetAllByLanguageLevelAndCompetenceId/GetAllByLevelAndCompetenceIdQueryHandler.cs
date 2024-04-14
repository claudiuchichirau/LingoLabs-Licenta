using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetAllByLanguageLevelAndCompetenceId
{
    internal class GetAllByLevelAndCompetenceIdQueryHandler : IRequestHandler<GetAllByLevelAndCompetenceIdQuery, GetAllByLevelAndCompetenceIdResponse>
    {
        private readonly IQuestionRepository questionRepository;
        public GetAllByLevelAndCompetenceIdQueryHandler(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;   
        }
        public async Task<GetAllByLevelAndCompetenceIdResponse> Handle(GetAllByLevelAndCompetenceIdQuery request, CancellationToken cancellationToken)
        {
            GetAllByLevelAndCompetenceIdResponse response = new();

            var result = await questionRepository.GetQuestionsByLanguageLevelAndCompetenceIdAsync(request.LanguageLevelId, request.LanguageCompetenceId);

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
