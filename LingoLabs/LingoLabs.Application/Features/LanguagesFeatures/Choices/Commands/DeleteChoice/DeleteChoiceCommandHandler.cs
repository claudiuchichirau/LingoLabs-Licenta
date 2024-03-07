using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.DeleteChoice
{
    public class DeleteChoiceCommandHandler: IRequestHandler<DeleteChoiceCommand, DeleteChoiceCommandResponse>
    {
        private readonly IChoiceRepository choiceRepository;

        public DeleteChoiceCommandHandler(IChoiceRepository choiceRepository)
        {
            this.choiceRepository = choiceRepository;
        }

        public async Task<DeleteChoiceCommandResponse> Handle(DeleteChoiceCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteChoiceCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new DeleteChoiceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var choice = await choiceRepository.FindByIdAsync(request.ChoiceId);
            if(!choice.IsSuccess)
            {
                return new DeleteChoiceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { choice.Error }
                };
            }

            await choiceRepository.DeleteAsync(request.ChoiceId);

            return new DeleteChoiceCommandResponse
            {
                Success = true
            };
        }
    }
}
