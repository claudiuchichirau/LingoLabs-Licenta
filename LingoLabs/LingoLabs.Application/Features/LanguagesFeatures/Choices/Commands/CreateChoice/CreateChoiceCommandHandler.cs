using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;
using MediatR;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.CreateChoice
{
    public class CreateChoiceCommandHandler : IRequestHandler<CreateChoiceCommand, CreateChoiceCommandResponse>
    {
        private readonly IChoiceRepository repository;
        public CreateChoiceCommandHandler(IChoiceRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateChoiceCommandResponse> Handle(CreateChoiceCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateChoiceCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateChoiceCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            var choice = Choice.Create(request.ChoiceContent, request.IsCorrect);
            if (choice.IsSuccess)
            {
                await repository.AddAsync(choice.Value);
                return new CreateChoiceCommandResponse
                {
                    Choice = new CreateChoiceDto
                    {
                        ChoiceId = choice.Value.ChoiceId,
                        ChoiceContent = choice.Value.ChoiceContent,
                        IsCorrect = choice.Value.IsCorrect
                    }
                };
            }

            return new CreateChoiceCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { choice.Error }
            };
        }
    }
}
