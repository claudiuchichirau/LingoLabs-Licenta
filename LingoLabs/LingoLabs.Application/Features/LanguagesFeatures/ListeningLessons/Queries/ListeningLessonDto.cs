using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Queries
{
    public class ListeningLessonDto
    {
        public Guid LessonId { get; set; }
        public string LessonTitle { get; set; } = default!;
        public LanguageCompetenceType LessonType { get; set; }
        public Guid ChapterId { get; set; }
        public string TextScript { get; set; } = string.Empty;
        public List<string> Accents { get; set; } = [];
    }
}
