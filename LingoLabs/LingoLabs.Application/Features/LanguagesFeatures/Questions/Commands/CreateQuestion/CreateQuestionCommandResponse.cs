using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandResponse: BaseResponse
    {
        public CreateQuestionCommandResponse(): base()
        {
        }

        public CreateQuestionDto Question { get; set; }
    }
}
