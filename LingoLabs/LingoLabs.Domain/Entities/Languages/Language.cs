using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Language : AuditableEntity
    {
        public Guid LanguageId { get; private set; }
        public string LanguageName { get; private set; }
        public string? LanguageDescription { get; private set; } = string.Empty;
        public string? LanguageVideoLink { get; private set; }= string.Empty;
        public byte[]? LanguageFlag { get; private set; } = [];
        public List<LanguageLevel>? LanguageLevels { get; private set; } = new();
        public List<LanguageCompetence>? LanguageCompetences { get; private set; } = new();
        public List<Question>? PlacementTest { get; private set; } = new();
        public List<EntityTag>? LanguageTags { get; private set; } = new();

        private Language(string languageName)
        {
            LanguageId = Guid.NewGuid();
            LanguageName = languageName;
        }

        public static Result<Language> Create(string languageName)
        {
            if (string.IsNullOrWhiteSpace(languageName))
                return Result<Language>.Failure("LanguageName is required");
            return Result<Language>.Success(new Language(languageName));
        }

        public void AttachDescription(string languageDescription)
        {
            if (!string.IsNullOrWhiteSpace(languageDescription))
                LanguageDescription = languageDescription;
        }

        public void AttachLanguageLevel(LanguageLevel languageLevel)
        {
            if (languageLevel != null)
            {
                if (LanguageLevels == null)
                    LanguageLevels = new List<LanguageLevel> { languageLevel };
                else
                    LanguageLevels.Add(languageLevel);
            }
        }

        public void AttachLanguageCompetence(LanguageCompetence languageCompetence)
        {
            if (languageCompetence != null)
            {
                if (LanguageCompetences == null)
                    LanguageCompetences = new List<LanguageCompetence> { languageCompetence };
                else
                    LanguageCompetences.Add(languageCompetence);
            }
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

        public void AttachKeyWord(EntityTag tag)
        {
            if (tag != null)
            {
                if (LanguageTags == null)
                    LanguageTags = new List<EntityTag> { tag };
                else
                    LanguageTags.Add(tag);
            }
        }

        public void AttachVideoLink(string languageVideoLink)
        {
            if (!string.IsNullOrWhiteSpace(languageVideoLink))
                LanguageVideoLink = languageVideoLink;
        }

        public void UpdateLanguage(string languageName, string languageDescription, string languageVideoLink, byte[] languageFlag)
        {
            if (!string.IsNullOrWhiteSpace(languageName))
                LanguageName = languageName;
            if (!string.IsNullOrWhiteSpace(languageDescription))
                LanguageDescription = languageDescription;
            if (!string.IsNullOrWhiteSpace(languageVideoLink))
                LanguageVideoLink = languageVideoLink;
            if (languageFlag.Length > 0)
                LanguageFlag = languageFlag;
        }
    }
}
