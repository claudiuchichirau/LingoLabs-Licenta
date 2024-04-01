using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries.GetById
{
    public class GetByIdLanguageCompetenceQueryHandler : IRequestHandler<GetByIdLanguageCompetenceQuery, GetSingleLanguageCompetenceDto>
    {
        private readonly ILanguageCompetenceRepository repository;
        private readonly IUserLanguageLevelRepository userLanguageLevelRepository;
        private readonly ITagRepository tagRepository;

        public GetByIdLanguageCompetenceQueryHandler(ILanguageCompetenceRepository repository, IUserLanguageLevelRepository userLanguageLevelRepository, ITagRepository tagRepository)
        {
            this.repository = repository;
            this.userLanguageLevelRepository = userLanguageLevelRepository;
            this.tagRepository = tagRepository;
        }
        public async Task<GetSingleLanguageCompetenceDto> Handle(GetByIdLanguageCompetenceQuery request, CancellationToken cancellationToken)
        {
            var languageCompetence = await repository.FindByIdAsync(request.Id);
            if(languageCompetence.IsSuccess)
            {
                var userLanguageLevelsResult = await userLanguageLevelRepository.FindByLanguageCompetenceIdAsync(request.Id);

                if (!userLanguageLevelsResult.IsSuccess)
                {
                    // Gestionați eroarea aici
                    return new GetSingleLanguageCompetenceDto();
                }

                var userLanguageLevels = userLanguageLevelsResult.Value;

                var sortedLessons = languageCompetence.Value.Lessons
                    .OrderBy(lesson => lesson.LessonPriorityNumber ?? int.MaxValue)
                    .Select(lesson => new Lessons.Queries.LessonDto
                    {
                        LessonId = lesson.LessonId,
                        LessonTitle = lesson.LessonTitle,
                        LessonType = lesson.LessonType,
                        ChapterId = lesson.ChapterId
                    }).ToList();

                var allTags = await tagRepository.GetAllAsync();

                var allTagsDto = allTags.Value.Select(tag => new Tags.Queries.TagDto
                {
                    TagId = tag.TagId,
                    TagContent = tag.TagContent
                });

                var tasks = languageCompetence.Value.LearningCompetenceTags.Select(async entityTag =>
                {
                    var tag = await tagRepository.FindByIdAsync(entityTag.TagId);
                    return new Tags.Queries.TagDto
                    {
                        TagId = entityTag.Tag.TagId,
                        TagContent = tag.Value.TagContent
                    };
                });
                var languageCompetenceKeyWords = (await Task.WhenAll(tasks)).ToList();

                var unassociatedTags = allTagsDto.Where(tag => !languageCompetenceKeyWords.Any(lkw => lkw.TagId == tag.TagId)).ToList();

                return new GetSingleLanguageCompetenceDto
                {
                    LanguageCompetenceId = languageCompetence.Value.LanguageCompetenceId,
                    LanguageCompetenceName = languageCompetence.Value.LanguageCompetenceName,
                    LanguageCompetenceType = languageCompetence.Value.LanguageCompetenceType,
                    LanguageId = languageCompetence.Value.LanguageId,
                    LanguageCompetenceDescription = languageCompetence.Value.LanguageCompetenceDescription,
                    LanguageCompetenceVideoLink = languageCompetence.Value.LanguageCompetenceVideoLink,
                    LanguageCompetencePriorityNumber = languageCompetence.Value.LanguageCompetencePriorityNumber,

                    LearningCompetenceKeyWords = languageCompetenceKeyWords,

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

            return new GetSingleLanguageCompetenceDto();
        }
    }
}
