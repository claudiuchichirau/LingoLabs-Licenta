using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries
{
    public class LessonDto
    {
        public Guid LessonId { get; set; }
        public string LessonTitle { get; set; } = default!;
        public Guid ChapterId { get; set; }
        public Guid LanguageCompetenceId { get; set; }
    }
}
