using LingoLabs.Application.Features.LanguagesFeatures.Questions.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries.GetById
{
    public class GetSingleLessonDto: LessonDto
    {
        public string LessonRequirement { get; set; } = string.Empty;      
        public string LessonVideoLink { get; set; } = string.Empty;
        public string LessonImageData { get; set; } = string.Empty;
        public string ChapterName { get; set; } = string.Empty;
        public string LanguageCompetenceName { get; set; } = string.Empty;
        public string LanguageLevelName { get; set; } = string.Empty;
        public List<QuestionDto> LessonQuestions { get; set; } = [];
        public List<TagDto> LessonTags { get; set; } = [];
        public List<TagDto> UnassociatedTags { get; set; } = [];
    }
}
