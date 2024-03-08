using LingoLabs.Application.Persistence.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.UpdateChoice
{
    public class UpdateChoiceCommandHandler : IRequestHandler<UpdateChoiceCommand, UpdateChoiceCommandResponse>
    {
        private readonly IChoiceRepository choiceRepository;

        public UpdateChoiceCommandHandler(IChoiceRepository choiceRepository)
        {
            this.choiceRepository = choiceRepository;
        }
        public async Task<UpdateChoiceCommandResponse> Handle(UpdateChoiceCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateChoiceCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid)
            {
                return new UpdateChoiceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var choice = await choiceRepository.FindByIdAsync(request.ChoiceId);

            if(!choice.IsSuccess)
            {
                return new UpdateChoiceCommandResponse 
                {
                    Success = false,
                    ValidationsErrors = new List<string> { choice.Error }
                };
            }

            var updateChoiceDto = request.UpdateChoiceDto;

            choice.Value.UpdateChoice(updateChoiceDto.ChoiceContent, updateChoiceDto.IsCorrect);

            await choiceRepository.UpdateAsync(choice.Value);

            return new UpdateChoiceCommandResponse
            {
                Success = true,
                UpdateChoiceDto = new UpdateChoiceDto
                {
                    ChoiceContent = choice.Value.ChoiceContent,
                    IsCorrect = choice.Value.IsCorrect
                }
            };
        }
    }
}
