using LingoLabs.Domain.Common;

namespace LingoLabs.Domain.Entities.Languages
{
    public class Chapter : AuditableEntity
    {
        public Guid ChapterId { get; private set; }
        public string ChapterName { get; private set; }
        public string? ChapterDescription { get; private set; } = string.Empty;
        public int? ChapterPriorityNumber { get; private set; }
        public string? ChapterImageData { get; private set; } = string.Empty;
        public string? ChapterVideoLink { get; private set; } = string.Empty;
        public List<Lesson>? ChapterLessons { get; private set; } = [];
        public List<EntityTag>? ChapterTags { get; private set; } = [];
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

        public void UpdateChapter(string chapterName, string chapterDescription, string chapterImageData, string chapterVideoLink, int? chapterPrioriryNumber)
        {
            if (!string.IsNullOrWhiteSpace(chapterName))
                ChapterName = chapterName;
            if (!string.IsNullOrWhiteSpace(chapterDescription))
                ChapterDescription = chapterDescription;
            if (!string.IsNullOrWhiteSpace(chapterImageData))
                ChapterImageData = chapterImageData;
            if (!string.IsNullOrWhiteSpace(chapterVideoLink))
                ChapterVideoLink = chapterVideoLink;
            ChapterPriorityNumber = chapterPrioriryNumber;
        }
    }
}
