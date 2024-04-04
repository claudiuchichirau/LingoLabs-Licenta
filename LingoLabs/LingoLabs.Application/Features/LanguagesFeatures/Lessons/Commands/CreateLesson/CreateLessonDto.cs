using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateLesson
{
    public class CreateLessonDto
    {
        public Guid LessonId { get; set; }
        public string? LessonTitle { get; set; }
        public Guid? ChapterId { get; set; }
        public Guid? LanguageCompetenceId { get; set; }
    }
}
