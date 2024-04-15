using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetAllByLanguageLevelAndCompetenceId
{
    public class GetAllByLevelAndCompetenceIdQuery : IRequest<GetAllByLevelAndCompetenceIdResponse>
    {
        public Guid LanguageLevelId { get; set; }
        public Guid LanguageCompetenceId { get; set; }
    }
}
