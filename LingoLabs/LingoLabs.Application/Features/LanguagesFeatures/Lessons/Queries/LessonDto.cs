using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries
{
    public class LessonDto
    {
        public Guid LessonId { get; set; }
        public int? LessonPriorityNumber { get; set; }
        public string LessonTitle { get; set; } = default!;
        public string? LessonDescription { get; set; } = string.Empty;
        public string? LessonContent { get; set; } = string.Empty;
        public Guid ChapterId { get; set; }
        public Guid LanguageCompetenceId { get; set; }
    }
}
