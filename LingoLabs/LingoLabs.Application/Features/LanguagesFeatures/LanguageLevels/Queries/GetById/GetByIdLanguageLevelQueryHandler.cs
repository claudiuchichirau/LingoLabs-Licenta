using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries.GetById;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries.GetById
{
    public class GetByIdLanguageLevelQueryHandler : IRequestHandler<GetByIdLanguageLevelQuery, LanguageLevelDto>
    {
        private readonly ILanguageLevelRepository repository;
        private readonly IUserLanguageLevelRepository userLanguageLevelRepository;
        private readonly ITagRepository tagRepository;

        public GetByIdLanguageLevelQueryHandler(ILanguageLevelRepository repository, IUserLanguageLevelRepository userLanguageLevelRepository, ITagRepository tagRepository)
        {
            this.repository = repository;
            this.userLanguageLevelRepository = userLanguageLevelRepository;
            this.tagRepository = tagRepository;
        }
        public async Task<LanguageLevelDto> Handle(GetByIdLanguageLevelQuery request, CancellationToken cancellationToken)
        {
            var languageLevel = await repository.FindByIdAsync(request.Id);
            if(languageLevel.IsSuccess)
            {
                var userLanguageLevelsResult = await userLanguageLevelRepository.FindByLanguageLevelIdAsync(request.Id);

                if (!userLanguageLevelsResult.IsSuccess)
                {
                    // Gestionați eroarea aici
                    return new GetSingleLanguageLevelDto();
                }

                var userLanguageLevels = userLanguageLevelsResult.Value;

                var languageChapterSorted = languageLevel.Value.LanguageLevelChapters
                    .OrderBy(chapter => chapter.ChapterPriorityNumber ?? int.MaxValue)
                    .Select(chapter => new Chapters.Queries.ChapterDto
                    {
                        ChapterId = chapter.ChapterId,
                        ChapterPriorityNumber = chapter.ChapterPriorityNumber,
                        ChapterName = chapter.ChapterName,
                        ChapterDescription = chapter.ChapterDescription,
                        ChapterVideoLink = chapter.ChapterVideoLink,
                        ChapterImageData = chapter.ChapterImageData,
                        ChapterLessons = chapter.ChapterLessons.Select(lesson => new Lessons.Queries.LessonDto
                        {
                            LessonId = lesson.LessonId,
                            LessonPriorityNumber = lesson.LessonPriorityNumber,
                            LessonTitle = lesson.LessonTitle,
                            LessonDescription = lesson.LessonDescription,
                            LessonContent = lesson.LessonContent,
                            LanguageCompetenceId = lesson.LanguageCompetenceId,
                        }).ToList(),
                        LanguageLevelId = chapter.LanguageLevelId
                    }).ToList();

                var allTags = await tagRepository.GetAllAsync();

                var allTagsDto = allTags.Value.Select(tag => new Tags.Queries.TagDto
                {
                    TagId = tag.TagId,
                    TagContent = tag.TagContent
                });

                var tasks = languageLevel.Value.LanguageLevelTags.Select(async entityTag =>
                {
                    var tag = await tagRepository.FindByIdAsync(entityTag.TagId);
                    return new Tags.Queries.TagDto
                    {
                        TagId = entityTag.Tag.TagId,
                        TagContent = tag.Value.TagContent
                    };
                });
                var languageLevelKeyWords = (await Task.WhenAll(tasks)).ToList();

                var unassociatedTags = allTagsDto.Where(tag => !languageLevelKeyWords.Any(lkw => lkw.TagId == tag.TagId)).ToList();

                return new GetSingleLanguageLevelDto
                {
                    LanguageLevelId = languageLevel.Value.LanguageLevelId,
                    LanguageLevelPriorityNumber = languageLevel.Value.PriorityNumber,
                    LanguageLevelName = languageLevel.Value.LanguageLevelName,
                    LanguageLevelAlias = languageLevel.Value.LanguageLevelAlias,
                    LanguageId = languageLevel.Value.LanguageId,
                    LanguageLevelDescription = languageLevel.Value.LanguageLevelDescription,
                    LanguageLevelVideoLink = languageLevel.Value.LanguageLevelVideoLink,

                    LanguageChapters = languageChapterSorted,

                    LanguageLeveKeyWords = languageLevelKeyWords,
                    UnassociatedTags = unassociatedTags,

                    UserLanguageLevels = userLanguageLevels.Select(userLanguageLevel => new EnrollmentsFeatures.UserLanguageLevels.Queries.UserLanguageLevelDto
                    {
                        UserLanguageLevelId = userLanguageLevel.UserLanguageLevelId,
                        EnrollmentId = userLanguageLevel.EnrollmentId,
                        LanguageLevelId = userLanguageLevel.LanguageLevelId,
                        LanguageCompetenceId = userLanguageLevel.LanguageCompetenceId
                    }).ToList()
                };
            }

            return new GetSingleLanguageLevelDto();
        }
    }
}
