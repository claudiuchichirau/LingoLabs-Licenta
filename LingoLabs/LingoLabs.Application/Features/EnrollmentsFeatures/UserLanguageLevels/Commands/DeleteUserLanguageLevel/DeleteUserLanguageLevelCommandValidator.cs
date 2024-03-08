using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.UserLanguageLevels.Commands.DeleteUserLanguageLevel
{
    public class DeleteUserLanguageLevelCommandValidator: AbstractValidator<DeleteUserLanguageLevelCommand>
    {
        public DeleteUserLanguageLevelCommandValidator()
        {
            RuleFor(x => x.UserLanguageLevelId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
