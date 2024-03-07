using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.LanguageLevelResults.Commands.DeleteLanguageLevelResult
{
    public class DeleteLanguageLevelResultCommandValidator: AbstractValidator<DeleteLanguageLevelResultCommand>
    {
        public DeleteLanguageLevelResultCommandValidator()
        {
            RuleFor(x => x.LanguageLevelResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
