using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.QuestionResults.Commands.CreateQuestionResult
{
    public class CreateQuestionResultCommandValidator: AbstractValidator<CreateQuestionResultCommand>
    {
        public CreateQuestionResultCommandValidator()
        {
            RuleFor(p => p.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");


            RuleFor(p => p.LessonResultId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
