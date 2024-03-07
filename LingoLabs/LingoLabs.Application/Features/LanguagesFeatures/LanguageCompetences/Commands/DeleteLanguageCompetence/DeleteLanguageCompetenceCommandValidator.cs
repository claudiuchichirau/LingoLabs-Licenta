using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.DeleteLanguageCompetence
{
    public class DeleteLanguageCompetenceCommandValidator: AbstractValidator<DeleteLanguageCompetenceCommand>
    {
        public DeleteLanguageCompetenceCommandValidator()
        {
            RuleFor(p => p.LanguageCompetenceId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
