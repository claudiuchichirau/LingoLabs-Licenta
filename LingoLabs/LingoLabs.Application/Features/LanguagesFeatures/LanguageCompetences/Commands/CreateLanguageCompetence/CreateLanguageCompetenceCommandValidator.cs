using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.CreateLanguageCompetence
{
    public class CreateLanguageCompetenceCommandValidator: AbstractValidator<CreateLanguageCompetenceCommand>
    {
        public CreateLanguageCompetenceCommandValidator()
        {
            RuleFor(p => p.LanguageCompetenceName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LanguageCompetenceType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
