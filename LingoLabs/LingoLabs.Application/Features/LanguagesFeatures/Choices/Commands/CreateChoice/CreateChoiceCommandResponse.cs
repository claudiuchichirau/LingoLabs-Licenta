using LingoLabs.Application.Responses;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.CreateChoice
{
    public class CreateChoiceCommandResponse : BaseResponse
    {
        public CreateChoiceCommandResponse() : base()
        {
        }

        public CreateChoiceDto Choice { get; set; } = default!;
    }
}
