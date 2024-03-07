using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.UpdateLanguageCompetenceResult
{
    public class UpdateLanguageCompetenceResultComandValidator: AbstractValidator<UpdateLanguageCompetenceResultCommand>
    {
        public UpdateLanguageCompetenceResultComandValidator()
        {
            RuleFor(p => p.LanguageCompetenceResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
