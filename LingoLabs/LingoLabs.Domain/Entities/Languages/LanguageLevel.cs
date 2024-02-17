using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class LanguageLevel : AuditableEntity
    {
        public Guid LanguageLevelId { get; private set; }
        public string LanguageLevelName { get; private set; } 
        public string LanguageLevelAlias { get; private set; } 
        public string? LanguageLevelDescription { get; private set; } = string.Empty;
        public string? LanguageLevelVideoLink { get; private set; } = string.Empty;
        public List<Chapter>? LanguageChapters { get; private set; } = new();
        public List<Tag>? LanguageLeveKeyWords { get; private set; } = new();
        public Guid LanguageId { get; private set; }
        public Language? Language { get; set; }

        private LanguageLevel() 
        {
            LanguageLevelId = Guid.Empty;
        }

        private LanguageLevel(string languageLevelName, string languageLevelAlias)
        {
            LanguageLevelId = Guid.NewGuid();
            LanguageLevelName = languageLevelName;
            LanguageLevelAlias = languageLevelAlias;
        }

        public static Result<LanguageLevel> Create(string languageLevelName, string languageLevelAlias)
        {
            if (string.IsNullOrWhiteSpace(languageLevelName))
                return Result<LanguageLevel>.Failure("LanguageLevelName is required");
            if (string.IsNullOrWhiteSpace(languageLevelAlias))
                return Result<LanguageLevel>.Failure("LanguageLevelAlias is required");
            return Result<LanguageLevel>.Success(new LanguageLevel(languageLevelName, languageLevelAlias));
        }

        public void AttachDescription(string languageLevelDescription)
        {
            if (!string.IsNullOrWhiteSpace(languageLevelDescription))
                LanguageLevelDescription = languageLevelDescription;
        }

        public void AttachLanguageChapter(Chapter chapter)
        {
            if (chapter != null)
            {
                if (LanguageChapters == null)
                    LanguageChapters = new List<Chapter> { chapter };
                else
                    LanguageChapters.Add(chapter);
            }
        }

        public void AttachKeyWord(Tag tag)
        {
            if (tag != null)
            {
                if (LanguageLeveKeyWords == null)
                    LanguageLeveKeyWords = new List<Tag> { tag };
                else
                    LanguageLeveKeyWords.Add(tag);
            }
        }

        public void AttachVideoLink(string languageLevelVideoLink)
        {
            if (!string.IsNullOrWhiteSpace(languageLevelVideoLink))
                LanguageLevelVideoLink = languageLevelVideoLink;
        }

        public void UpdateLanguage(string languageLevelName, string languageLevelAlias, string languageLevelDescription, string languageLevelVideoLink)
        {
            if (string.IsNullOrWhiteSpace(languageLevelName))
                LanguageLevelName = languageLevelName;
            if (string.IsNullOrWhiteSpace(languageLevelAlias))
                LanguageLevelAlias = languageLevelAlias;
            if (!string.IsNullOrWhiteSpace(languageLevelDescription))
                LanguageLevelDescription = languageLevelDescription;
            if (!string.IsNullOrWhiteSpace(languageLevelVideoLink))
                LanguageLevelVideoLink = languageLevelVideoLink;
        }
    }
}
