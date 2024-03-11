using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.DeleteQuiz
{
    public class DeleteQuizCommandValidator: AbstractValidator<DeleteQuizCommand>
    {
        public DeleteQuizCommandValidator()
        {
            RuleFor(x => x.LessonId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
