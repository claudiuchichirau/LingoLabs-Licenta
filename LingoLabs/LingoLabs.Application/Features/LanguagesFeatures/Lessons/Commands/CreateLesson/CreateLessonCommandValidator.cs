using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandValidator: AbstractValidator<CreateLessonCommand>
    {
        private readonly ILanguageCompetenceRepository _languageCompetenceRepository;
        public CreateLessonCommandValidator(ILanguageCompetenceRepository _languageCompetenceRepository)
        {
            this._languageCompetenceRepository = _languageCompetenceRepository;

            RuleFor(p => p.LessonTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LessonType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(type => type == LanguageCompetenceType.Grammar || type == LanguageCompetenceType.Reading || type == LanguageCompetenceType.Writing)
                .WithMessage("{PropertyName} must have one of the following values: Grammar, Reading, Writing");

            RuleFor(p => p)
                .MustAsync(async (command, cancellationToken) =>
                    command.LessonType == await _languageCompetenceRepository.GetLanguageCompetenceTypeAsync(command.LanguageCompetenceId))
                .WithMessage("{PropertyName} must be the same as the LanguageCompetenceType associated with the provided LanguageCompetenceId.");

            RuleFor(p => p.LanguageCompetenceId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required.");
        }
    }
}
