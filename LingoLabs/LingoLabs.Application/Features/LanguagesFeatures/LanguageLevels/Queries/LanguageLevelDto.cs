namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Queries
{
    public class LanguageLevelDto
    {
        public Guid LanguageLevelId { get; set; }
        public int? LanguageLevelPriorityNumber { get; set; }
        public string LanguageLevelName { get; set; } = string.Empty;
        public string LanguageLevelAlias { get; set; } = string.Empty;
        public string? LanguageLevelDescription { get; set; } = string.Empty;
        public string? LanguageLevelVideoLink { get; set; } = string.Empty;
        public Guid LanguageId { get; set; }
    }
}
