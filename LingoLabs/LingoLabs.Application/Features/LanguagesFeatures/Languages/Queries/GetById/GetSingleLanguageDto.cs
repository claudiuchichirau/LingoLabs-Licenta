using LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries.GetById;
using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Queries.GetById
{
    public class GetSingleLanguageDto: LanguageDto
    {
        public List<LanguageLevelDto> LanguageLevels { get; set; } = [];
        public List<LanguageCompetenceDto> LanguageCompetences { get; set; } = [];
        public List<GetSingleQuestionDto> PlacementTest { get; set; } = [];
        public List<TagDto> LanguageKeyWords { get; set; } = [];
        public List<TagDto> UnassociatedTags { get; set; } = [];
    }
}
