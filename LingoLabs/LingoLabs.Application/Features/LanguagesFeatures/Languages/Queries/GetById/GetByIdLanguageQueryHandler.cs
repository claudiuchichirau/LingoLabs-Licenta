using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Queries.GetById
{
    public class GetByIdLanguageQueryHandler : IRequestHandler<GetByIdLanguageQuery, GetSingleLanguageDto>
    {
        private readonly ILanguageRepository repository;

        public GetByIdLanguageQueryHandler(ILanguageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetSingleLanguageDto> Handle(GetByIdLanguageQuery request, CancellationToken cancellationToken)
        {
            var language = await repository.FindByIdAsync(request.Id);
            if (language.IsSuccess)
            {
                return new GetSingleLanguageDto
                {
                    LanguageId = language.Value.LanguageId,
                    LanguageName = language.Value.LanguageName,
                    LanguageDescription = language.Value.LanguageDescription,
                    LanguageVideoLink = language.Value.LanguageVideoLink,

                    LanguageLevels = language.Value.LanguageLevels.Select(languageLevel => new LanguageLevels.Queries.LanguageLevelDto
                    {
                        LanguageLevelId = languageLevel.LanguageLevelId,
                        LanguageLevelName = languageLevel.LanguageLevelName,
                        LanguageLevelAlias = languageLevel.LanguageLevelAlias,
                        LanguageId = languageLevel.LanguageId
                    }).ToList(),

                    LanguageCompetences = language.Value.LanguageCompetences.Select(languageCompetence => new LanguageCompetences.Queries.LanguageCompetenceDto
                    {
                        LanguageCompetenceId = languageCompetence.LanguageCompetenceId,
                        LanguageCompetenceName = languageCompetence.LanguageCompetenceName,
                        LanguageCompetenceType = languageCompetence.LanguageCompetenceType,
                        ChapterId = languageCompetence.ChapterId,
                        LanguageId = languageCompetence.LanguageId
                    }).ToList(),

                    LanguageKeyWords = language.Value.LanguageKeyWords.Select(tag => new Tags.Queries.TagDto
                    {
                        TagId = tag.TagId,
                        TagContent = tag.TagContent
                    }).ToList(),

                    PlacementTest = language.Value.PlacementTest.Select(question => new Questions.Queries.QuestionDto
                    {
                        QuestionId = question.QuestionId,
                        QuestionRequirement = question.QuestionRequirement,
                        QuestionLearningType = question.QuestionLearningType,
                        LessonId = question.LessonId
                    }).ToList()
                };
            }

            return new GetSingleLanguageDto();
        }
    }
}
