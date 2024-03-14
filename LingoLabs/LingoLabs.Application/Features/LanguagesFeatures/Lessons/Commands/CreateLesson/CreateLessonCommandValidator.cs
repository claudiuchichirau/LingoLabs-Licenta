using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandValidator: AbstractValidator<CreateLessonCommand>
    {
        private readonly ILanguageCompetenceRepository _languageCompetenceRepository;
        private readonly ILessonRepository lessonRepository;
        public CreateLessonCommandValidator(ILanguageCompetenceRepository _languageCompetenceRepository, ILessonRepository lessonRepository)
        {
            this._languageCompetenceRepository = _languageCompetenceRepository;
            this.lessonRepository = lessonRepository;

            RuleFor(p => p.LessonTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLesson(p.LessonTitle, lessonRepository))
                .WithMessage("{PropertyName} must be unique.");

            RuleFor(p => p.LessonType)
                //.NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(type => type == LanguageCompetenceType.Grammar || type == LanguageCompetenceType.Reading || type == LanguageCompetenceType.Writing)
                .WithMessage("{PropertyName} must have one of the following values: Grammar, Reading, Writing");

            RuleFor(p => p)
                .MustAsync(async (command, cancellationToken) =>
                    command.LessonType == await _languageCompetenceRepository.GetLanguageCompetenceTypeAsync(command.LanguageCompetenceId))
                .WithMessage("LessonType must be the same as the LanguageCompetenceType associated with the provided LanguageCompetenceId.");

            RuleFor(p => p.LanguageCompetenceId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> ValidateLesson(string lessonTitle, ILessonRepository lessonRepository)
        {

            if (await lessonRepository.ExistsLessonAsync(lessonTitle) == true)
                return false;
            return true;
        }
    }
}
