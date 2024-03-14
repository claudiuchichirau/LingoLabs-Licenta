namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.UpdateListeningLesson
{
    public class UpdateListeningLessonDto
    {
        public string LessonTitle { get; set; } = string.Empty;
        public string LessonDescription { get; set; } = string.Empty;
        public string LessonRequirement { get; set; } = string.Empty;
        public string LessonContent { get; set; } = string.Empty;
        public string LessonVideoLink { get; set; } = string.Empty;
        public byte[] LessonImageData { get; set; } = [];
        public int? LessonPriorityNumber { get; set; }
        public string TextScript { get; set; } = string.Empty;
        public List<string> Accents { get; set; } = [];
    }
}
