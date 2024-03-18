using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries.GetById
{
    public class GetSingleLessonDto: LessonDto
    {
        public string LessonDescription { get; set; } = string.Empty;
        public string LessonRequirement { get; set; } = string.Empty;      
        public string LessonContent { get; set; } = string.Empty;
        public string LessonVideoLink { get; set; } = string.Empty;
        public byte[] LessonImageData { get; set; } = [];
        public int? LessonPriorityNumber { get; set; }
        public List<QuestionDto> LessonQuestions { get; set; } = [];
        public List<TagDto> LessonTags { get; set; } = [];
        public List<TagDto> UnassociatedTags { get; set; } = [];
    }
}
