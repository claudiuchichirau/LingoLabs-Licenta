using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Queries.GetById
{
    public class GetSingleListeningLessonDto: ListeningLessonDto
    {
        public string LessonRequirement { get; set; } = string.Empty;
        public string LessonVideoLink { get; set; } = string.Empty;
        public string LessonImageData { get; set; } = string.Empty;
        public List<QuestionDto> LessonQuestions { get; set; } = [];
    }
}
