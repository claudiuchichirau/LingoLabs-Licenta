using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.DeleteLanguageLevel
{
    public class DeleteLanguageLevelCommandValidator: AbstractValidator<DeleteLanguageLevelCommand>
    {
        public DeleteLanguageLevelCommandValidator()
        {
            RuleFor(p => p.LanguageLevelId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
