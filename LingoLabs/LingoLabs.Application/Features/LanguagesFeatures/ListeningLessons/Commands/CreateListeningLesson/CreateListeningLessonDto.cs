using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.CreateListeningLesson
{
    public class CreateListeningLessonDto
    {
        public Guid LessonId { get; set; }
        public string? LessonTitle { get; set; }
        public LanguageCompetenceType? LessonType { get; set; }
        public Guid? ChapterId { get; set; }
        public string? TextScript { get; set; } = string.Empty;
        public List<string>? Accents { get; set; }
    }
}
