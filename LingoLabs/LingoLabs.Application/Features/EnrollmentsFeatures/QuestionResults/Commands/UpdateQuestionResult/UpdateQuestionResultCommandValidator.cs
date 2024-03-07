using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.UpdateQuestionResult
{
    public class UpdateQuestionResultCommandValidator: AbstractValidator<UpdateQuestionResultCommand>
    {
        public UpdateQuestionResultCommandValidator()
        {
            RuleFor(p => p.QuestionResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
