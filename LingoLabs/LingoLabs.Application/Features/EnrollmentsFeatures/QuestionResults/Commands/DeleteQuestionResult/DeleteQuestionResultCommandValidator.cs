using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.DeleteQuestionResult
{
    public class DeleteQuestionResultCommandValidator: AbstractValidator<DeleteQuestionResultCommand>
    {
        public DeleteQuestionResultCommandValidator()
        {
            RuleFor(p => p.QuestionResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
