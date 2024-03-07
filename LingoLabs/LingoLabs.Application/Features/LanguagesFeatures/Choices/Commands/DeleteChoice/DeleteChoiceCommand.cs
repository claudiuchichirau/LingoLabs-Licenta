using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.DeleteChoice
{
    public class DeleteChoiceCommand: IRequest<DeleteChoiceCommandResponse>
    {
        public Guid ChoiceId { get; set; }
    }
}
