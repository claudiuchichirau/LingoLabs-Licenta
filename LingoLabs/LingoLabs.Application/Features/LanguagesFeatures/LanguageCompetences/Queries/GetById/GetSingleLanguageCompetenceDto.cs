using LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Chapters.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Lessons.Queries;
using LingoLabs.Application.Features.LanguagesFeatures.Tags.Queries;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Queries.GetById
{
    public class GetSingleLanguageCompetenceDto: LanguageCompetenceDto
    {
        public List<TagDto> LearningCompetenceKeyWords { get; set; } = [];
        public List<TagDto> UnassociatedTags { get; set; } = [];
        public List<UserLanguageLevelDto> UserLanguageLevels { get; set; } = [];
        public List<LessonDto> LanguageCompetenceLessons { get; set; } = [];
    }
}
