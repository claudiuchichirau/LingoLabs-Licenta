using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries.GetById
{
    public class GetByIdLanguageLevelQueryHandler : IRequestHandler<GetByIdLanguageLevelQuery, LanguageLevelDto>
    {
        private readonly ILanguageLevelRepository repository;

        public GetByIdLanguageLevelQueryHandler(ILanguageLevelRepository repository)
        {
            this.repository = repository;
        }
        public async Task<LanguageLevelDto> Handle(GetByIdLanguageLevelQuery request, CancellationToken cancellationToken)
        {
            var languageLevel = await repository.FindByIdAsync(request.Id);
            if(languageLevel.IsSuccess)
            {
                return new GetSingleLanguageLevelDto
                {
                    LanguageLevelId = languageLevel.Value.LanguageLevelId,
                    LanguageLevelName = languageLevel.Value.LanguageLevelName,
                    LanguageLevelAlias = languageLevel.Value.LanguageLevelAlias,
                    LanguageId = languageLevel.Value.LanguageId,
                    LanguageLevelDescription = languageLevel.Value.LanguageLevelDescription,
                    LanguageLevelVideoLink = languageLevel.Value.LanguageLevelVideoLink,

                    LanguageChapters = languageLevel.Value.LanguageChapters.Select(chapter => new Chapters.Queries.ChapterDto
                    {
                        ChapterId = chapter.ChapterId,
                        ChapterName = chapter.ChapterName,
                        LanguageLevelId = chapter.LanguageLevelId
                    }).ToList(),

                    LanguageLeveKeyWords = languageLevel.Value.LanguageLeveKeyWords.Select(tag => new Tags.Queries.TagDto
                    {
                        TagId = tag.TagId,
                        TagContent = tag.TagContent
                    }).ToList()
                };
            }

            return new GetSingleLanguageLevelDto();
        }
    }
}
