namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.CreateLanguage
{
    public class CreateLanguageDto
    {
        public Guid LanguageId { get; set; }
        public string? LanguageName { get; set; }
        public string? LanguageFlag { get; set; }
    }
}
