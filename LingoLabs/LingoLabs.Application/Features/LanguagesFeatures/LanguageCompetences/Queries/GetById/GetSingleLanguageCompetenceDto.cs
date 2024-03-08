using LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries.GetById
{
    public class GetSingleLanguageCompetenceDto: LanguageCompetenceDto
    {
        public string LanguageCompetenceDescription { get; set; } = string.Empty;
        public string LanguageCompetenceVideoLink { get; set; } = string.Empty;
        public List<LessonDto> Lessons { get; set; } = [];
        public List<TagDto> LearningCompetenceKeyWords { get; set; } = [];
    }
}
