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

            if (result.IsSuccess)
            {
                var languages = new List<LanguageDto>();

                foreach (var language in result.Value)
                {
                    var languageLevelCount = await repository.GetLanguageLevelCountAsync(language.LanguageId);
                    var lessonCount = await repository.GetLessonCountAsync(language.LanguageId);

                    languages.Add(new LanguageDto
                    {
                        LanguageId = language.LanguageId,
                        LanguageName = language.LanguageName,
                        LanguageFlag = language.LanguageFlag,
                        LanguageDescription = language.LanguageDescription,
                        LanguageVideoLink = language.LanguageVideoLink,
                        LanguageLevelCount = languageLevelCount,
                        LessonCount = lessonCount
                    });
                }

                response.Languages = languages;
            }

            return response;
        }
    }
}
