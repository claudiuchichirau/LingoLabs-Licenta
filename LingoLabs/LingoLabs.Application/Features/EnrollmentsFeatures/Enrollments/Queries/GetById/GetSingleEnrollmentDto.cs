using LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Queries;
using LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Queries;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.Enrollments.Queries.GetById
{
    public class GetSingleEnrollmentDto: EnrollmentDto
    {
        public List<LanguageLevelResultDto> LanguageLevelResults { get; set; } = [];
        public List<UserLanguageLevelDto> UserLanguageLevels { get; set; } = [];
    }
}
