using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.CreateUserLanguageLevel
{
    public class CreateUserLanguageLevelCommandValidator: AbstractValidator<CreateUserLanguageLevelCommand>
    {
        public CreateUserLanguageLevelCommandValidator()
        {
            RuleFor(p => p.EnrollmentId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required");

            RuleFor(p => p.LanguageCompetenceId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required");

            RuleFor(p => p.LanguageLevelId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required");
        }
    }
}
