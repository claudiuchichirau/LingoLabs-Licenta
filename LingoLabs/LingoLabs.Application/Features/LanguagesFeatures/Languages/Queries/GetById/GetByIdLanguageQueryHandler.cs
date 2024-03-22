using LingoLabs.Application.Persistence.Languages;
using MediatR;
using System.Linq;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Queries.GetById
{
    public class GetByIdLanguageQueryHandler : IRequestHandler<GetByIdLanguageQuery, GetSingleLanguageDto>
    {
        private readonly ILanguageRepository repository;
        private readonly ITagRepository tagRepository;

        public GetByIdLanguageQueryHandler(ILanguageRepository repository, ITagRepository tagRepository)
        {
            this.repository = repository;
            this.tagRepository = tagRepository;
        }

        public async Task<GetSingleLanguageDto> Handle(GetByIdLanguageQuery request, CancellationToken cancellationToken)
        {
            var language = await repository.FindByIdAsync(request.Id);
            if (language.IsSuccess)
            {
                var sortedLanguageLevels = language.Value.LanguageLevels
                    .OrderBy(languageLevel => languageLevel.PriorityNumber ?? int.MaxValue)
                    .Select(languageLevel => new LanguageLevels.Queries.LanguageLevelDto
                    {
                        LanguageLevelId = languageLevel.LanguageLevelId,
                        LanguageLevelName = languageLevel.LanguageLevelName,
                        LanguageLevelAlias = languageLevel.LanguageLevelAlias,
                        LanguageId = languageLevel.LanguageId
                    }).ToList();

                var sortedLanguageCompetences = language.Value.LanguageCompetences
                    .OrderBy(languageCompetence => languageCompetence.LanguageCompetencePriorityNumber ?? int.MaxValue)
                    .Select(languageCompetence => new LanguageCompetences.Queries.LanguageCompetenceDto
                    {
                        LanguageCompetenceId = languageCompetence.LanguageCompetenceId,
                        LanguageCompetenceName = languageCompetence.LanguageCompetenceName,
                        LanguageCompetenceType = languageCompetence.LanguageCompetenceType,
                        ChapterId = languageCompetence.ChapterId,
                        LanguageId = languageCompetence.LanguageId
                    }).ToList();

                var allTags = await tagRepository.GetAllAsync();

                var allTagsDto = allTags.Value.Select(tag => new Tags.Queries.TagDto
                {
                    TagId = tag.TagId,
                    TagContent = tag.TagContent
                });

                var tasks = language.Value.LanguageTags.Select(async entityTag =>
                {
                    var tag = await tagRepository.FindByIdAsync(entityTag.TagId);
                    return new Tags.Queries.TagDto
                    {
                        TagId = entityTag.Tag.TagId,
                        TagContent = tag.Value.TagContent
                    };
                });
                var languageKeyWords = (await Task.WhenAll(tasks)).ToList();

                var unassociatedTags = allTagsDto.Where(tag => !languageKeyWords.Any(lkw => lkw.TagId == tag.TagId)).ToList();

                return new GetSingleLanguageDto
                {
                    LanguageId = language.Value.LanguageId,
                    LanguageName = language.Value.LanguageName,
                    LanguageDescription = language.Value.LanguageDescription,
                    LanguageVideoLink = language.Value.LanguageVideoLink,
                    LanguageFlag = language.Value.LanguageFlag,

                    LanguageLevels = sortedLanguageLevels,

                    LanguageCompetences = sortedLanguageCompetences,

                    LanguageKeyWords = languageKeyWords,

                    UnassociatedTags = unassociatedTags,

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
