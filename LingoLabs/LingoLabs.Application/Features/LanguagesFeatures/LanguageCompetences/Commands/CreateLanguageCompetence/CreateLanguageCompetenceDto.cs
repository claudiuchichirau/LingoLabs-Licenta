using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.CreateLanguageCompetence
{
    public class CreateLanguageCompetenceDto
    {
        public Guid LanguageCompetenceId { get; set; }
        public string? LanguageCompetenceName { get; set; }
        public LanguageCompetenceType? LanguageCompetenceType { get; set; }
        public Guid? ChapterId { get; set; }
        public Guid? LanguageId { get; set; }
    }
}
