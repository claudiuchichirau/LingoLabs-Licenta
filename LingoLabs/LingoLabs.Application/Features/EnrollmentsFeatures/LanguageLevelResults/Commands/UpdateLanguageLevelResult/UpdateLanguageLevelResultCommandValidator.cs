using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.UpdateLanguageLevelResult
{
    public class UpdateLanguageLevelResultCommandValidator: AbstractValidator<UpdateLanguageLevelResultCommand>
    {
        public UpdateLanguageLevelResultCommandValidator()
        {
            RuleFor(p => p.LanguageLevelResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
