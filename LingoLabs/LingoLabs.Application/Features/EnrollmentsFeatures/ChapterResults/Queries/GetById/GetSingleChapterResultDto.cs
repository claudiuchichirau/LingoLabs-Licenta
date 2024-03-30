using LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Queries;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Queries.GetById
{
    public class GetSingleChapterResultDto: ChapterResultDto
    {
        public List<LessonResultDto> LessonResults { get;  set; } = [];
    }
}
