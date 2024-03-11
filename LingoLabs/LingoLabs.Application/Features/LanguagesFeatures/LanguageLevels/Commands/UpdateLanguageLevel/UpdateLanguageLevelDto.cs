using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.UpdateLanguageLevel
{
    public class UpdateLanguageLevelDto
    {
        public string LanguageLevelAlias { get; set; } = string.Empty;
        public string LanguageLevelDescription { get; set; } = string.Empty;
        public string LanguageLevelVideoLink { get; set; } = string.Empty;
    }
}
