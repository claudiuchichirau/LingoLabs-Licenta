using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateQuiz
{
    public class UpdateQuizCommand: IRequest<UpdateQuizCommandResponse>
    {
        public Guid LessonId { get; set; }
        public List<UpdateQuestionForDto> UpdateQuizDto { get; set; } = [];
    }
}
