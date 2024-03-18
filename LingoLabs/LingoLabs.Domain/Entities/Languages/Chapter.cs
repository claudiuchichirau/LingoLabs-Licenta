using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Chapter : AuditableEntity
    {
        public Guid ChapterId { get; private set; }
        public string ChapterName { get; private set; }
        public string? ChapterDescription { get; private set; } = string.Empty;
        public int? ChapterPriorityNumber { get; private set; }
        public byte[]? ChapterImageData { get; private set; }
        public string? ChapterVideoLink { get; private set; } = string.Empty;
        public List<LanguageCompetence>? languageCompetences { get; private set; } = new();
        public List<EntityTag>? ChapterTags { get; private set; } = new();
        public Guid LanguageLevelId { get; private set; }
        public LanguageLevel? LanguageLevel { get; set; }

        private Chapter(string chapterName, Guid languageLevelId)
        {
            ChapterId = Guid.NewGuid();
            ChapterName = chapterName;
            LanguageLevelId = languageLevelId;
        }

        public static Result<Chapter> Create(string chapterName, Guid languageLevelId)
        {
            if (string.IsNullOrWhiteSpace(chapterName))
                return Result<Chapter>.Failure("ChapterName is required");
            if (languageLevelId == default)
                return Result<Chapter>.Failure("LanguageLevelId is required");
            return Result<Chapter>.Success(new Chapter(chapterName, languageLevelId));
        }

        public void AttachDescription(string chapterDescription)
        {
            if (!string.IsNullOrWhiteSpace(chapterDescription))
                ChapterDescription = chapterDescription;
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

        public void AttachTag(EntityTag entityTag)
        {
            if (entityTag != null)
            {
                if (ChapterTags == null)
                    ChapterTags = new List<EntityTag> { entityTag };
                else
                    ChapterTags.Add(entityTag);
            }
        }

        public void UpdateChapter(string chapterName, string chapterDescription, byte[] chapterImageData, string chapterVideoLink, int? chapterPrioriryNumber)
        {
            if (!string.IsNullOrWhiteSpace(chapterName))
                ChapterName = chapterName;
            if (!string.IsNullOrWhiteSpace(chapterDescription))
                ChapterDescription = chapterDescription;
            if (chapterImageData != null && chapterImageData.Length > 0)
                ChapterImageData = chapterImageData;
            if (!string.IsNullOrWhiteSpace(chapterVideoLink))
                ChapterVideoLink = chapterVideoLink;
            ChapterPriorityNumber = chapterPrioriryNumber;
        }
    }
}
