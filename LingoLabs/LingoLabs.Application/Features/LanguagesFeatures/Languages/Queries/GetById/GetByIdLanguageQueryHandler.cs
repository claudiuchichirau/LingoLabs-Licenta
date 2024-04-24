﻿using LingoLabs.Application.Persistence.Languages;
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
                        LanguageLevelPriorityNumber = languageLevel.PriorityNumber,
                        LanguageLevelName = languageLevel.LanguageLevelName,
                        LanguageLevelAlias = languageLevel.LanguageLevelAlias,
                        LanguageLevelDescription = languageLevel.LanguageLevelDescription,
                        LanguageLevelVideoLink = languageLevel.LanguageLevelVideoLink,
                        LanguageId = languageLevel.LanguageId
                    }).ToList();

                var sortedLanguageCompetences = language.Value.LanguageCompetences
                    .OrderBy(languageCompetence => languageCompetence.LanguageCompetencePriorityNumber ?? int.MaxValue)
                    .Select(languageCompetence => new LanguageCompetences.Queries.LanguageCompetenceDto
                    {
                        LanguageCompetenceId = languageCompetence.LanguageCompetenceId,
                        LanguageCompetencePriorityNumber = languageCompetence.LanguageCompetencePriorityNumber,
                        LanguageCompetenceName = languageCompetence.LanguageCompetenceName,
                        LanguageCompetenceType = languageCompetence.LanguageCompetenceType,
                        LanguageCompetenceDescription = languageCompetence.LanguageCompetenceDescription,
                        LanguageCompetenceVideoLink = languageCompetence.LanguageCompetenceVideoLink,
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

                    PlacementTest = language.Value.PlacementTest.Select(question => new Questions.Queries.GetById.GetSingleQuestionDto
                    {
                        QuestionId = question.QuestionId,
                        QuestionRequirement = question.QuestionRequirement,
                        QuestionType = question.QuestionType,
                        LessonId = question.LessonId,
                        Choices = question.Choices.Select(choice => new Choices.Queries.ChoiceDto
                        {
                            ChoiceId = choice.ChoiceId,
                            ChoiceContent = choice.ChoiceContent,
                            IsCorrect = choice.IsCorrect,
                            QuestionId = choice.QuestionId
                        }).ToList()
                    }).ToList()
                };
            }

            return new GetSingleLanguageDto();
        }
    }
}
