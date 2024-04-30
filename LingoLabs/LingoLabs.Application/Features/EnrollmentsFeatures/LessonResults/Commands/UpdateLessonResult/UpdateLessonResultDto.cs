using LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Queries;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.UpdateLessonResult
{
    public class UpdateLessonResultDto
    {
        public bool IsCompleted { get; set; }
        public List<QuestionResultDto> QuestionResults { get; set; } = new List<QuestionResultDto>();
    }
}
