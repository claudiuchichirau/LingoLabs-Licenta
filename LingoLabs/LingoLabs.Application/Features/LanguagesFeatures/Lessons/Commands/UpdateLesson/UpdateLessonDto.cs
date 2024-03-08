using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateLesson
{
    public class UpdateLessonDto
    {
        public string LessonTitle { get; set; } = string.Empty;
        public string LessonDescription { get; set; } = string.Empty;
        public string LessonRequirement { get;  set; } = string.Empty;
        public string LessonContent { get; set; } = string.Empty;
        public string LessonVideoLink { get; set; } = string.Empty;
        public byte[] LessonImageData { get; set; } = [];
    }
}
