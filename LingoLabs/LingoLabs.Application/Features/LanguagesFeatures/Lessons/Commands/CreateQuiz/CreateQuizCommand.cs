using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz
{
    public class CreateQuizCommand: IRequest<CreateQuizCommandResponse>
    {
        public Guid LessonId { get; set; }
        public List<QuestionDtoForQuiz> QuestionList { get; set; } = [];
    }
}
