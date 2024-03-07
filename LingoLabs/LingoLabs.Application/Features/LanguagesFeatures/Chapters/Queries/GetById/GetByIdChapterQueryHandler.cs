using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries;
using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries.GetById
{
    public class GetByIdChapterQueryHandler : IRequestHandler<GetByIdChapterQuery, GetSingleChapterDto>
    {
        private readonly IChapterRepository repository;

        public GetByIdChapterQueryHandler(IChapterRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetSingleChapterDto> Handle(GetByIdChapterQuery request, CancellationToken cancellationToken)
        {
            var chapter = await repository.FindByIdAsync(request.Id);
            if(chapter.IsSuccess)
            {
                return new GetSingleChapterDto
                {
                    ChapterId = chapter.Value.ChapterId,
                    ChapterName = chapter.Value.ChapterName,
                    LanguageLevelId = chapter.Value.LanguageLevelId,
                    ChapterDescription = chapter.Value.ChapterDescription,
                    ChapterNumber = chapter.Value.ChapterNumber ?? 0,
                    ChapterImageData = chapter.Value.ChapterImageData,
                    ChapterVideoLink = chapter.Value.ChapterVideoLink,

                    languageCompetences = chapter.Value.languageCompetences.Select(languageCompetence => new LanguageCompetences.Queries.LanguageCompetenceDto
                    {
                        LanguageCompetenceId = languageCompetence.LanguageCompetenceId,
                        LanguageCompetenceName = languageCompetence.LanguageCompetenceName,
                        LanguageCompetenceType = languageCompetence.LanguageCompetenceType,
                        ChapterId = languageCompetence.ChapterId,
                        LanguageId = languageCompetence.LanguageId
                    }).ToList(),

                    ChapterKeyWords = chapter.Value.ChapterKeyWords.Select(tag => new Tags.Queries.TagDto
                    {
                        TagId = tag.TagId,
                        TagContent = tag.TagContent
                    }).ToList()
                };
            }

            return new GetSingleChapterDto();
        }
    }
}
