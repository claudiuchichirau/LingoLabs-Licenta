using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.CreateListeningLesson
{
    public class CreateListeningLessonDto
    {
        public Guid LessonId { get; set; }
        public string? LessonTitle { get; set; }
        public LanguageCompetenceType? LessonType { get; set; }
        public List<byte[]>? AudioContents { get; set; }
        public List<string>? Accents { get; set; }
    }
}
