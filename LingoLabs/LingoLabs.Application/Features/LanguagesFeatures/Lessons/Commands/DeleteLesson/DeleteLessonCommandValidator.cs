using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandValidator: AbstractValidator<DeleteLessonCommand>
    {
        public DeleteLessonCommandValidator()
        {
            RuleFor(x => x.LessonId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
