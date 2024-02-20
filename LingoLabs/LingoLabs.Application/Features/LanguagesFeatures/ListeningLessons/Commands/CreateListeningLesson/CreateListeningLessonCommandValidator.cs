using FluentValidation;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.CreateListeningLesson
{
    public class CreateListeningLessonCommandValidator : AbstractValidator<CreateListeningLessonCommand>
    {
        public CreateListeningLessonCommandValidator()
        {
            RuleFor(p => p.LessonTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters.");

            RuleFor(p => p.LessonType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(type => type == LanguageCompetenceType.Grammar || type == LanguageCompetenceType.Listening || type == LanguageCompetenceType.Reading || type == LanguageCompetenceType.Writing)
                .WithMessage("{PropertyName} must have one of the following values: Grammar, Listening, Reading, Writing");

            RuleFor(p => p.LanguageCompetenceId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.AudioContents)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.Accents)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
