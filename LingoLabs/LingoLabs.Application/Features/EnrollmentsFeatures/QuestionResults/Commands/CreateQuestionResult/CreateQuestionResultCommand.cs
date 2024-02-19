using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.CreateQuestionResult
{
    public class CreateQuestionResultCommand: IRequest<CreateQuestionResultCommandResponse>
    {
        public Guid QuestionId { get; set; }
        public Guid LessonResultId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
