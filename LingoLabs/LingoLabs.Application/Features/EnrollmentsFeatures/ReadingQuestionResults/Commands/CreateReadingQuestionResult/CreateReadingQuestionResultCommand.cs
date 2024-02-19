using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ReadingQuestionResults.Commands.CreateReadingQuestionResult
{
    public class CreateReadingQuestionResultCommand: IRequest<CreateReadingQuestionResultCommandResponse>
    {
        public Guid QuestionId { get; set; }
        public Guid LessonResultId { get; set; }
        public bool IsCorrect { get; set; }
        public byte[] AudioData { get; set; } = default!;
    }
}
