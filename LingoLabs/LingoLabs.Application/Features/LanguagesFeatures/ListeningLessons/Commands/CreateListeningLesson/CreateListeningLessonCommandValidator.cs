using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.ListeningLessons.Commands.CreateListeningLesson
{
    public class CreateListeningLessonCommandValidator : AbstractValidator<CreateListeningLessonCommand>
    {
        private readonly ILanguageCompetenceRepository _languageCompetenceRepository;
        public CreateListeningLessonCommandValidator(ILanguageCompetenceRepository _languageCompetenceRepository)
        {
            RuleFor(p => p.LessonTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters.");

            RuleFor(p => p.LessonType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(type => type == LanguageCompetenceType.Listening)
                .WithMessage("{PropertyName} must be Listening");

            RuleFor(p => p)
                .MustAsync(async (command, cancellationToken) =>
                    command.LessonType == await _languageCompetenceRepository.GetLanguageCompetenceTypeAsync(command.LanguageCompetenceId))
                .WithMessage("{PropertyName} must be the same as the LanguageCompetenceType associated with the provided LanguageCompetenceId.");

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
