using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetAllByLanguageCompetenceId
{
    public class GetAllQuestionsByLanguageCompetenceIdQuery: IRequest<GetAllQuestionsByLanguageCompetenceIdResponse>
    {
        public Guid LanguageCompetenceId { get; set; }
    }
}
