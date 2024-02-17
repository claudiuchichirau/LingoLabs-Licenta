using FluentValidation;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandValidator: AbstractValidator<CreateLessonCommand>
    {
        public CreateLessonCommandValidator()
        {
            RuleFor(p => p.LessonTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LessonType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
