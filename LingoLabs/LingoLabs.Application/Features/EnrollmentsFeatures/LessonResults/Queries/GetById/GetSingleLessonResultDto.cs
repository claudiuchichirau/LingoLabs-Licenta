using LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Queries;
using LingoLabs.Domain.Entities.Enrollments;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Queries.GetById
{
    public class GetSingleLessonResultDto: LessonResultDto
    {
        public List<QuestionResultDto>? QuestionResults { get; set; } = [];
    }
}
