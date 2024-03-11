using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries.GetById
{
    public class GetByIdLanguageCompetenceQueryHandler : IRequestHandler<GetByIdLanguageCompetenceQuery, GetSingleLanguageCompetenceDto>
    {
        private readonly ILanguageCompetenceRepository repository;
        private readonly IUserLanguageLevelRepository userLanguageLevelRepository;

        public GetByIdLanguageCompetenceQueryHandler(ILanguageCompetenceRepository repository, IUserLanguageLevelRepository userLanguageLevelRepository)
        {
            this.repository = repository;
            this.userLanguageLevelRepository = userLanguageLevelRepository;
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

                return new GetSingleLanguageCompetenceDto
                {
                    LanguageCompetenceId = languageCompetence.Value.LanguageCompetenceId,
                    LanguageCompetenceName = languageCompetence.Value.LanguageCompetenceName,
                    LanguageCompetenceType = languageCompetence.Value.LanguageCompetenceType,
                    ChapterId = languageCompetence.Value.ChapterId,
                    LanguageId = languageCompetence.Value.LanguageId,
                    LanguageCompetenceDescription = languageCompetence.Value.LanguageCompetenceDescription,
                    LanguageCompetenceVideoLink = languageCompetence.Value.LanguageCompetenceVideoLink,

                    Lessons = languageCompetence.Value.Lessons.Select(lesson => new Lessons.Queries.LessonDto
                    {
                        LessonId = lesson.LessonId,
                        LessonTitle = lesson.LessonTitle,
                        LessonType = lesson.LessonType,
                        LanguageCompetenceId = lesson.LanguageCompetenceId
                    }).ToList(),

                    LearningCompetenceKeyWords = languageCompetence.Value.LearningCompetenceKeyWords.Select(tag => new Tags.Queries.TagDto
                    {
                        TagId = tag.TagId,
                        TagContent = tag.TagContent
                    }).ToList(),

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
