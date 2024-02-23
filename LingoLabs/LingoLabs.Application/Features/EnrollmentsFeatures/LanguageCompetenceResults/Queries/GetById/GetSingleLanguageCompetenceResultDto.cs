using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Queries;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Queries.GetById
{
    public class GetSingleLanguageCompetenceResultDto: LanguageCompetenceResultDto
    {
        public List<LessonResultDto> LessonsResults { get; set; } = [];
    }
}
