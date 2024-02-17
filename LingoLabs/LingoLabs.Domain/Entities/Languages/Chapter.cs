using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Chapter : AuditableEntity
    {
        public Guid ChapterId { get; private set; }
        public string ChapterName { get; private set; }
        public string? ChapterDescription { get; private set; } = string.Empty;
        public int? ChapterNumber { get; private set; } = null;
        public byte[]? ChapterImageData { get; private set; }
        public string? ChapterVideoLink { get; private set; } = string.Empty;
        public List<LanguageCompetence>? languageCompetences { get; private set; } = new();
        public List<Tag>? ChapterKeyWords { get; private set; } = new();
        public Guid LanguageLevelId { get; private set; }
        public LanguageLevel? LanguageLevel { get; set; }

        private Chapter(string chapterName)
        {
            ChapterId = Guid.NewGuid();
            ChapterName = chapterName;
        }

        public static Result<Chapter> Create(string chapterName)
        {
            if (string.IsNullOrWhiteSpace(chapterName))
                return Result<Chapter>.Failure("ChapterName is required");
            return Result<Chapter>.Success(new Chapter(chapterName));
        }

        public void AttachDescription(string chapterDescription)
        {
            if (!string.IsNullOrWhiteSpace(chapterDescription))
                ChapterDescription = chapterDescription;
        }

        public void AttachChapterNumber(int chapterNumber)
        {
            if (chapterNumber > 0)
                ChapterNumber = chapterNumber;
        }

        public void AttachLanguageCompetence(LanguageCompetence languageCompetence)
        {
            if (languageCompetence != null)
            {
                if (languageCompetences == null)
                    languageCompetences = new List<LanguageCompetence> { languageCompetence };
                else
                    languageCompetences.Add(languageCompetence);
            }
        }

        public void AttachKeyWord(Tag tag)
        {
            if (tag != null)
            {
                if (ChapterKeyWords == null)
                    ChapterKeyWords = new List<Tag> { tag };
                else
                    ChapterKeyWords.Add(tag);
            }
        }

        public void AttachImageData(byte[] chapterImageData)
        {
            if (chapterImageData != null && chapterImageData.Length > 0)
                ChapterImageData = chapterImageData;
        }

        public void AttachVideoLink(string chapterVideoLink)
        {
            if (!string.IsNullOrWhiteSpace(chapterVideoLink))
                ChapterVideoLink = chapterVideoLink;
        }

        public void UpdateChapter(string chapterName, string chapterDescription, int chapterNumber, byte[] chapterImageData, string chapterVideoLink)
        {
            if (!string.IsNullOrWhiteSpace(chapterName))
                ChapterName = chapterName;
            if (!string.IsNullOrWhiteSpace(chapterDescription))
                ChapterDescription = chapterDescription;
            if (chapterNumber > 0)
                ChapterNumber = chapterNumber;
            if (chapterImageData != null && chapterImageData.Length > 0)
                ChapterImageData = chapterImageData;
            if (!string.IsNullOrWhiteSpace(chapterVideoLink))
                ChapterVideoLink = chapterVideoLink;
        }
    }
}
