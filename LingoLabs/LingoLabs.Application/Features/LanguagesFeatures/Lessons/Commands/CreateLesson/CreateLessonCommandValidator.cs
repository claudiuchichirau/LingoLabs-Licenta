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
                .NotNull()
                .Must(type => type == LanguageCompetenceType.Grammar || type == LanguageCompetenceType.Listening || type == LanguageCompetenceType.Reading || type == LanguageCompetenceType.Writing)
                .WithMessage("{PropertyName} must have one of the following values: Grammar, Listening, Reading, Writing");
        }
    }
}
