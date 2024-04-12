using LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries
{
    public class LanguageCompetenceDto
    {
        public Guid LanguageCompetenceId { get; set; }
        public int? LanguageCompetencePriorityNumber { get; set; }
        public string LanguageCompetenceName { get; set; } = string.Empty;
        public LanguageCompetenceType LanguageCompetenceType { get; set; }
        public string? LanguageCompetenceDescription { get; set; } = string.Empty;
        public string? LanguageCompetenceVideoLink { get; set; } = string.Empty;
        public Guid LanguageId { get; set; }
    }
}
