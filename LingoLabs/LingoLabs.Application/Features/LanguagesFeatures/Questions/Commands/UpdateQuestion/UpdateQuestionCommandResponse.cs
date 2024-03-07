using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandResponse: BaseResponse
    {
        public UpdateQuestionDto? UpdateQuestion { get; set; }
    }
}
