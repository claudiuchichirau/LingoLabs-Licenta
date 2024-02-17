using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.CreateChoice
{
    public class CreateChoiceCommandValidator: AbstractValidator<CreateChoiceCommand>
    {
        public CreateChoiceCommandValidator()
        {
            RuleFor(p => p.ChoiceContent)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
            RuleFor(p => p.IsCorrect)
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
