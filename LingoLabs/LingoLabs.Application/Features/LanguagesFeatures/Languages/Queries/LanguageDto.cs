namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Queries
{
    public class LanguageDto
    {
        public Guid LanguageId { get; set; }
        public string LanguageName { get; set; } = default!;
        public string LanguageFlag { get; set; } = default!;
        public string? LanguageDescription { get; set; } = default!;
        public string? LanguageVideoLink { get; set; } = default!;
        public int LanguageLevelCount { get; set; }
        public int LessonCount { get; set; }
    }
}
