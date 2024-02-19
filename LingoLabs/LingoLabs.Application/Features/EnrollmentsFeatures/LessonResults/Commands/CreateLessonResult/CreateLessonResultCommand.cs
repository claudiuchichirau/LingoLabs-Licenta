using MediatR;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LessonResults.Commands.CreateLessonResult
{
    public class CreateLessonResultCommand: IRequest<CreateLessonResultCommandResponse>
    {
        public Guid LessonId { get; set; }
        public Guid LanguageCompetenceResultId { get; set; }
    }
}
