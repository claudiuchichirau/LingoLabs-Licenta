using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Queries.GetById
{
    public class GetSingleLanguageDto: LanguageDto
    {
        public string LanguageDescription { get; set; } = string.Empty;
        public string LanguageVideoLink { get; set; } = string.Empty;
        public List<LanguageLevelDto> LanguageLevels { get; set; } = [];
        public List<LanguageCompetenceDto> LanguageCompetences { get; set; } = [];
        public List<QuestionDto> PlacementTest { get; set; } = [];
        public List<TagDto> LanguageKeyWords { get; set; } = [];
    }
}
