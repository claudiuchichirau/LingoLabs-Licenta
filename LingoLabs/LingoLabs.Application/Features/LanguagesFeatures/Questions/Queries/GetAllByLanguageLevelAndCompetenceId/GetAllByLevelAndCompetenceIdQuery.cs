using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetAllByLanguageLevelAndCompetenceId
{
    internal class GetAllByLevelAndCompetenceIdQuery : IRequest<GetAllByLevelAndCompetenceIdResponse>
    {
        public Guid LanguageLevelId { get; set; }
        public Guid LanguageCompetenceId { get; set; }
    }
}
