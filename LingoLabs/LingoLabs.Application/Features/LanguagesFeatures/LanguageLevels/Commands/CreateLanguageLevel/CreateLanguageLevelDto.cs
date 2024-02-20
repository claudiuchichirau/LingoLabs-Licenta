namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.CreateLanguageLevel
{
    public class CreateLanguageLevelDto
    {
        public Guid LanguageLevelId { get; set; }
        public string? LanguageLevelName { get; set; }
        public string? LanguageLevelAlias { get; set; }
        public Guid? LanguageId { get;  set; }
    }
}
