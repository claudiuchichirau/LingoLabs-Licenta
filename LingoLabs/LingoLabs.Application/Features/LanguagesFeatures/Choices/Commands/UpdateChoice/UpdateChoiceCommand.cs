using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.UpdateChoice
{
    public class UpdateChoiceCommand: IRequest<UpdateChoiceCommandResponse>
    {
        public Guid ChoiceId { get; set; }
        public UpdateChoiceDto UpdateChoiceDto { get; set; } = new UpdateChoiceDto();
    }
}
