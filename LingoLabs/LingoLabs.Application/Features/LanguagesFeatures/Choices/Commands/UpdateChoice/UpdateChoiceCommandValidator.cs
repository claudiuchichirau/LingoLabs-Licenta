using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Choices.Commands.UpdateChoice
{
    public class UpdateChoiceCommandValidator: AbstractValidator<UpdateChoiceCommand>
    {
        public UpdateChoiceCommandValidator()
        {
            RuleFor(p => p.ChoiceContent)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.IsCorrect)
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
