using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries.GetAll
{
    public class GetAllLanguageLevelsQueryHandler : IRequestHandler<GetAllLanguageLevelsQuery, GetAllLanguageLevelsResponse>
    {
        private readonly ILanguageLevelRepository repository;

        public GetAllLanguageLevelsQueryHandler(ILanguageLevelRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetAllLanguageLevelsResponse> Handle(GetAllLanguageLevelsQuery request, CancellationToken cancellationToken)
        {
            GetAllLanguageLevelsResponse response = new();
            var result = await repository.GetAllAsync();
            if(result.IsSuccess)
            {
                response.LanguageLevels = result.Value.Select(languageLevel => new LanguageLevelDto
                {
                    LanguageLevelId = languageLevel.LanguageLevelId,
                    LanguageLevelName = languageLevel.LanguageLevelName,
                    LanguageLevelAlias = languageLevel.LanguageLevelAlias,
                    LanguageId = languageLevel.LanguageId
                }).ToList();
            }

            return response;
        }
    }
}
