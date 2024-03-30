using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries.GetAll
{
    public class GetAllLanguageCompetencesQueryHandler: IRequestHandler<GetAllLanguageCompetencesQuery, GetAllLanguageCompetencesResponse>
    {
        private readonly ILanguageCompetenceRepository repository;

        public GetAllLanguageCompetencesQueryHandler(ILanguageCompetenceRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetAllLanguageCompetencesResponse> Handle(GetAllLanguageCompetencesQuery request, CancellationToken cancellationToken)
        {
            GetAllLanguageCompetencesResponse response = new();
            var result = await repository.GetAllAsync();
            if(result.IsSuccess)
            {
                response.LanguageCompetences = result.Value.Select(x => new LanguageCompetenceDto
                {
                    LanguageCompetenceId = x.LanguageCompetenceId,
                    LanguageCompetenceName = x.LanguageCompetenceName,
                    LanguageCompetenceType = x.LanguageCompetenceType,
                    LanguageId = x.LanguageId
                }).ToList();
            }
            return response;
        }
    }
}
