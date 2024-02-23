using LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Queries;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Queries.GetById
{
    public class GetSingleLanguageLevelResultDto: LanguageLevelResultDto
    {
        public List<ChapterResultDto> ChapterResults { get; set; } = [];
    }
}
