using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.UpdateQuiz
{
    public class UpdateQuizCommandResponse: BaseResponse
    {
        public UpdateQuizDto? UpdateQuiz { get; set; } 
    }
}
