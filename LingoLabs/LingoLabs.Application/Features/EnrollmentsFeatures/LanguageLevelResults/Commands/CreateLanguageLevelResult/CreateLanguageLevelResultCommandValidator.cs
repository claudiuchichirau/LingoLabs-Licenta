using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.CreateLanguageLevelResult
{
    public class CreateLanguageLevelResultCommandValidator: AbstractValidator<CreateLanguageLevelResultCommand>
    {
        public CreateLanguageLevelResultCommandValidator()
        {
            RuleFor(p => p.LanguageLevelId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.EnrollmentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
