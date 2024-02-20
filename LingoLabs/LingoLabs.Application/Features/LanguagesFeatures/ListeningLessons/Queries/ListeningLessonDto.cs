using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Queries
{
    public class ListeningLessonDto
    {
        public Guid LessonId { get; set; }
        public string LessonTitle { get; set; } = default!;
        public LanguageCompetenceType LessonType { get; set; }
        public Guid LanguageCompetenceId { get; set; }
        public List<byte[]> AudioContents { get; set; } = [];
        public List<string> Accents { get; set; } = [];
    }
}
