using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries
{
    public class ChapterDto
    {
        public Guid ChapterId { get; set; }
        public int? ChapterPriorityNumber { get; set; }
        public string ChapterName { get; set; } = string.Empty;
        public string? ChapterDescription { get; set; } = string.Empty;
        public string? ChapterImageData { get; set; } = string.Empty;
        public string? ChapterVideoLink { get; set; } = string.Empty;
        public List<ListeningLessonDto>? ChapterLessons { get; set; } = [];
        public Guid LanguageLevelId { get; set; }
    }
}
