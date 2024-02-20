using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Queries.GetAll
{
    public class GetAllLanguagesQueryHandler : IRequestHandler<GetAllLanguagesQuery, GetAllLanguagesResponse>
    {
        private readonly ILanguageRepository repository;

        public GetAllLanguagesQueryHandler(ILanguageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllLanguagesResponse> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
        {
            GetAllLanguagesResponse response = new();
            var result = await repository.GetAllAsync();
            if(result.IsSuccess)
            {
                response.Languages = result.Value.Select(language => new LanguageDto
                {
                    LanguageId = language.LanguageId,
                    LanguageName = language.LanguageName
                }).ToList();
            }

            return response;
        }
    }
}
