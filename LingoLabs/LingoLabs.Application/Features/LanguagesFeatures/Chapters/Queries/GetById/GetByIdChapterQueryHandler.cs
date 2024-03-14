using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
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
                var sortedLanguageCompetences = chapter.Value.languageCompetences
                    .OrderBy(languageCompetence => languageCompetence.LanguageCompetencePriorityNumber ?? int.MaxValue)
                    .Select(languageCompetence => new LanguageCompetences.Queries.LanguageCompetenceDto
                    {
                        LanguageCompetenceId = languageCompetence.LanguageCompetenceId,
                        LanguageCompetenceName = languageCompetence.LanguageCompetenceName,
                        LanguageCompetenceType = languageCompetence.LanguageCompetenceType,
                        ChapterId = languageCompetence.ChapterId,
                        LanguageId = languageCompetence.LanguageId
                    }).ToList();

                return new GetSingleChapterDto
                {
                    ChapterId = chapter.Value.ChapterId,
                    ChapterName = chapter.Value.ChapterName,
                    LanguageLevelId = chapter.Value.LanguageLevelId,
                    ChapterDescription = chapter.Value.ChapterDescription,
                    ChapterPriorityNumber = chapter.Value.ChapterPriorityNumber,
                    ChapterImageData = chapter.Value.ChapterImageData,
                    ChapterVideoLink = chapter.Value.ChapterVideoLink,

                    languageCompetences = sortedLanguageCompetences,

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
