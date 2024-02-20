using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries
{
    public class LanguageCompetenceDto
    {
        public Guid LanguageCompetenceId { get; set; }
        public string LanguageCompetenceName { get; set; } = string.Empty;
        public LanguageCompetenceType LanguageCompetenceType { get; set; }
        public Guid ChapterId { get; set; }
        public Guid LanguageId { get; set; }
    }
}
