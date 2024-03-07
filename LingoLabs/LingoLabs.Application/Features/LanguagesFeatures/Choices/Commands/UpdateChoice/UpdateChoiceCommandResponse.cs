using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.UpdateChoice
{
    public class UpdateChoiceCommandResponse: BaseResponse
    {
        public UpdateChoiceDto? UpdateChoiceDto { get; set; }
    }
}
