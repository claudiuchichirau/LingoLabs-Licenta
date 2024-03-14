using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Queries.GetById
{
    public class GetSingleListeningLessonDto: ListeningLessonDto
    {
        public string LessonDescription { get; set; } = string.Empty;
        public string LessonRequirement { get; set; } = string.Empty;
        public string LessonContent { get; set; } = string.Empty;
        public string LessonVideoLink { get; set; } = string.Empty;
        public byte[] LessonImageData { get; set; } = [];
        public int? LessonPriorityNumber { get; set; }
        public List<QuestionDto> LessonQuestions { get; set; } = [];
    }
}
