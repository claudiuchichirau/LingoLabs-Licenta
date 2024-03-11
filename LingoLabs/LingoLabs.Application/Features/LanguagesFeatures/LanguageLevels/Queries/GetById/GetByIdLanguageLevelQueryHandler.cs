using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries.GetById;
using LingoLabs.Application.Persistence.Enrollments;
using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries.GetById
{
    public class GetByIdLanguageLevelQueryHandler : IRequestHandler<GetByIdLanguageLevelQuery, LanguageLevelDto>
    {
        private readonly ILanguageLevelRepository repository;
        private readonly IUserLanguageLevelRepository userLanguageLevelRepository;

        public GetByIdLanguageLevelQueryHandler(ILanguageLevelRepository repository, IUserLanguageLevelRepository userLanguageLevelRepository)
        {
            this.repository = repository;
            this.userLanguageLevelRepository = userLanguageLevelRepository;
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

            return new GetSingleLanguageLevelDto();
        }
    }
}
