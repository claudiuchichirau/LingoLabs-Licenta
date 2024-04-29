using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries.GetById
{
    public class GetByIdChapterQueryHandler : IRequestHandler<GetByIdChapterQuery, GetSingleChapterDto>
    {
        private readonly IChapterRepository repository;
        private readonly ITagRepository tagRepository;
        private readonly ILanguageLevelRepository languageLevelRepository;

        public GetByIdChapterQueryHandler(IChapterRepository repository, ITagRepository tagRepository, ILanguageLevelRepository languageLevelRepository)
        {
            this.repository = repository;
            this.tagRepository = tagRepository;
            this.languageLevelRepository = languageLevelRepository;
        }
        public async Task<GetSingleChapterDto> Handle(GetByIdChapterQuery request, CancellationToken cancellationToken)
        {
            var chapter = await repository.FindByIdAsync(request.Id);
            if(chapter.IsSuccess)
            {
                var allTags = await tagRepository.GetAllAsync();

                var allTagsDto = allTags.Value.Select(tag => new Tags.Queries.TagDto
                {
                    TagId = tag.TagId,
                    TagContent = tag.TagContent
                });

                var tasks = chapter.Value.ChapterTags.Select(async entityTag =>
                {
                    var tag = await tagRepository.FindByIdAsync(entityTag.TagId);
                    return new Tags.Queries.TagDto
                    {
                        TagId = entityTag.Tag.TagId,
                        TagContent = tag.Value.TagContent
                    };
                });
                var chapterKeyWords = (await Task.WhenAll(tasks)).ToList();

                var unassociatedTags = allTagsDto.Where(tag => !chapterKeyWords.Any(lkw => lkw.TagId == tag.TagId)).ToList();

                var languageLevel = await languageLevelRepository.FindByIdAsync(chapter.Value.LanguageLevelId);

                var lessonsSorted = chapter.Value.ChapterLessons
                    .OrderBy(lesson => lesson.LessonPriorityNumber ?? int.MaxValue)
                    .Select(lesson => new ListeningLessons.Queries.ListeningLessonDto
                    {
                        LessonId = lesson.LessonId,
                        LessonPriorityNumber = lesson.LessonPriorityNumber,
                        LessonTitle = lesson.LessonTitle,
                        LessonDescription = lesson.LessonDescription,
                        LessonContent = lesson.LessonContent,
                        LessonVideoLink = lesson.LessonVideoLink,
                        LessonImageData = lesson.LessonImageData,
                        ChapterId = lesson.ChapterId,
                        LanguageCompetenceId = lesson.LanguageCompetenceId
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
                    LanguageLevelName = languageLevel.Value.LanguageLevelName,

                    ChapterLessons = lessonsSorted,

                    ChapterKeyWords = chapterKeyWords,
                    UnassociatedTags = unassociatedTags
                };
            }

            return new GetSingleChapterDto();
        }
    }
}
