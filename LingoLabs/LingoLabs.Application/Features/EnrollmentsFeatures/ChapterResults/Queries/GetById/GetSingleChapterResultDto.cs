using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Queries;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Queries.GetById
{
    public class GetSingleChapterResultDto: ChapterResultDto
    {
        public List<LanguageCompetenceResultDto> LanguageCompetenceResults { get;  set; } = [];
    }
}
