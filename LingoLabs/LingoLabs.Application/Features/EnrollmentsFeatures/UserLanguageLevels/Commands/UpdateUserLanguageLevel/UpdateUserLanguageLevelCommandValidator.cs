using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.UpdateUserLanguageLevel
{
    public class UpdateUserLanguageLevelCommandValidator: AbstractValidator<UpdateUserLanguageLevelCommand>
    {
        public UpdateUserLanguageLevelCommandValidator()
        {
            RuleFor(p => p.UserLanguageLevelId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.UpdateUserLanguageLevel.LanguageLevelId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
