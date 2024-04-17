using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.UpdateChoice
{
    public class UpdateChoiceCommand: IRequest<UpdateChoiceCommandResponse>
    {
        public Guid ChoiceId { get; set; }
        public string ChoiceContent { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
