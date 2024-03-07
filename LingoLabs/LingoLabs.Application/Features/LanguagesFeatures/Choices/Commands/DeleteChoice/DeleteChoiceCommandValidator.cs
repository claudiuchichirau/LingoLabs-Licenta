using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.DeleteChoice
{
    public class DeleteChoiceCommandValidator: AbstractValidator<DeleteChoiceCommand>
    {
        public DeleteChoiceCommandValidator()
        {
            RuleFor(p => p.ChoiceId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
