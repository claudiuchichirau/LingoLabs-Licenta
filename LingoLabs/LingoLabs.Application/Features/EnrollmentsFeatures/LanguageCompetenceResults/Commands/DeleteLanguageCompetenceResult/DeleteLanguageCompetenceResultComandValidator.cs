using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.DeleteLanguageCompetenceResult
{
    public class DeleteLanguageCompetenceResultComandValidator: AbstractValidator<DeleteLanguageCompetenceResultCommand>
    {
        public DeleteLanguageCompetenceResultComandValidator()
        {
            RuleFor(x => x.LanguageCompetenceResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
