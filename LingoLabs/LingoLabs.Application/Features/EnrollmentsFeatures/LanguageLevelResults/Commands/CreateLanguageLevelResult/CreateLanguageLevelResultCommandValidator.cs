using FluentValidation;
using LingoLabs.Application.Persistence.Enrollments;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.CreateLanguageLevelResult
{
    public class CreateLanguageLevelResultCommandValidator: AbstractValidator<CreateLanguageLevelResultCommand>
    {
        private readonly ILanguageLevelResultRepository repository;
        public CreateLanguageLevelResultCommandValidator(ILanguageLevelResultRepository repository)
        {
            this.repository = repository;

            RuleFor(p => p.LanguageLevelId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLanguageLevelResult(p.LanguageLevelId, p.EnrollmentId))
                .WithMessage("{PropertyName} must have one of the following values: Grammar, Listening, Reading, Writing and must be the same as LanguageCompetenceName");

            RuleFor(p => p.EnrollmentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> ValidateLanguageLevelResult(Guid languageLevelId, Guid enrollmentId)
        {
            if (await repository.CheckLanguageLevel(enrollmentId, languageLevelId))
                return false;

            return true;
        }
    }
}
