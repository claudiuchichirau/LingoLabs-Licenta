using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.UpdateLanguageCompetence
{
    public class UpdateLanguageCompetenceDto
    {
        public string LanguageCompetenceDescription { get; set; } = string.Empty;
        public string LanguageCompetenceVideoLink { get; set; } = string.Empty;
    }
}
