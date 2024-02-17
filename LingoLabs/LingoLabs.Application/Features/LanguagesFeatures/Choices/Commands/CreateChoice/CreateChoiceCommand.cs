using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.CreateChoice
{
    public class CreateChoiceCommand: IRequest<CreateChoiceCommandResponse>
    {
        public string ChoiceContent { get; set; }
        public bool IsCorrect { get; set; }
    }
}
