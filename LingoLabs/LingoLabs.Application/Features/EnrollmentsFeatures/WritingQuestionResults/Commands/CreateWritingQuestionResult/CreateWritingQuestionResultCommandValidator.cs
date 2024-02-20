using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.WritingQuestionResults.Commands.CreateWritingQuestionResult
{
    public class CreateWritingQuestionResultCommandValidator : AbstractValidator<CreateWritingQuestioResultCommand>
    {
        public CreateWritingQuestionResultCommandValidator()
        {
            RuleFor(p => p.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.LessonResultId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.IsCorrect)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ImageData)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .When(p => p.RecognizedText == null)
                .WithMessage("Either {PropertyName} or RecognizedText must be provided, but not both.");

            RuleFor(p => p.RecognizedText)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .When(p => p.ImageData == null)
                .WithMessage("Either {PropertyName} or ImageData must be provided, but not both.")
                .MaximumLength(2000).WithMessage("{PropertyName} must not exceed 2000 characters.");
        }
    }
}
