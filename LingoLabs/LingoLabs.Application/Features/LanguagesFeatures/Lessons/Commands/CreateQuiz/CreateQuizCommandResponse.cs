using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateQuiz
{
    public class CreateQuizCommandResponse: BaseResponse
    {
        public CreateQuizCommandResponse() : base()
        {
        }

        public QuizDto Quiz { get; set; }
    }
}
