namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries
{
    public class ChapterDto
    {
        public Guid ChapterId { get; set; }
        public string ChapterName { get; set; } = string.Empty;
        public Guid LanguageLevelId { get; set; }
    }
}
