using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageCompetenceResults.Commands.CreateLanguageCompetenceResult
{
    public class CreateLanguageCompResultCommandValidator: AbstractValidator<CreateLanguageCompetenceResultCommand>
    {
        public CreateLanguageCompResultCommandValidator()
        {
            RuleFor(p => p.LanguageCompetenceId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.ChapterResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
