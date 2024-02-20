namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Commands.CreateChapter
{
    public class CreateChapterDto
    {
        public Guid ChapterId { get; set; }
        public string? ChapterName { get; set; }
        public Guid? LanguageLevelId { get; set; }
    }
}
