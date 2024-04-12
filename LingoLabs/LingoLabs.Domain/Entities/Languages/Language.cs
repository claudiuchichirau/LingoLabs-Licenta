using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Language : AuditableEntity
    {
        public Guid LanguageId { get; private set; }
        public string LanguageName { get; private set; }
        public string? LanguageDescription { get; private set; } = string.Empty;
        public string? LanguageVideoLink { get; private set; }= string.Empty;
        public string LanguageFlag { get; private set; }
        public List<LanguageLevel>? LanguageLevels { get; private set; } = new();
        public List<LanguageCompetence>? LanguageCompetences { get; private set; } = new();
        public List<Question>? PlacementTest { get; private set; } = new();
        public List<EntityTag>? LanguageTags { get; private set; } = new();

        private Language(string languageName, string languageFlag)
        {
            LanguageId = Guid.NewGuid();
            LanguageName = languageName;
            LanguageFlag = languageFlag;
        }

        public static Result<Language> Create(string languageName, string languageFlag)
        {
            if (string.IsNullOrWhiteSpace(languageName))
                return Result<Language>.Failure("LanguageName is required");
            if (string.IsNullOrWhiteSpace(languageFlag))
                return Result<Language>.Failure("LanguageFlag is required");
            return Result<Language>.Success(new Language(languageName, languageFlag));
        }

        public void UpdatePlacementTest(Question question)
        {
            if (question != null)
            {
                if (PlacementTest == null)
                    PlacementTest = new List<Question> { question };
                else
                    PlacementTest.Add(question);
            }
        }

        public void UpdateLanguage(string languageName, string languageDescription, string languageVideoLink, string languageFlag)
        {
            if (!string.IsNullOrWhiteSpace(languageName))
                LanguageName = languageName;
            if (!string.IsNullOrWhiteSpace(languageDescription))
                LanguageDescription = languageDescription;
            if (!string.IsNullOrWhiteSpace(languageVideoLink))
                LanguageVideoLink = languageVideoLink;
            if (!string.IsNullOrWhiteSpace(languageFlag))
                LanguageFlag = languageFlag;
        }
    }
}
