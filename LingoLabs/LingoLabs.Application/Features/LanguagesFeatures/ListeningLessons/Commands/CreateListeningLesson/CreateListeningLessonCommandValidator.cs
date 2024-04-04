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
            this._languageCompetenceRepository = _languageCompetenceRepository;

            RuleFor(p => p.LessonTitle)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLanguageCompetence(p.LanguageCompetenceId, _languageCompetenceRepository))
                .WithMessage("LanguageCompetenceType must be Listening");

            RuleFor(p => p.ChapterId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.TextScript)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");

            RuleFor(p => p.Accents)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(accents => accents.Count >= 2).WithMessage("{PropertyName} must contain at least 2 elements.");

        }

        private async Task<bool> ValidateLanguageCompetence(Guid languageCompetenceId, ILanguageCompetenceRepository _languageCompetenceRepository)
        {
            var languageCompetence = await _languageCompetenceRepository.FindByIdAsync(languageCompetenceId);
            if (languageCompetence == null)
                return false;

            LanguageCompetenceType languageCompetenceType = await _languageCompetenceRepository.GetLanguageCompetenceTypeAsync(languageCompetenceId);
            if (languageCompetenceType == LanguageCompetenceType.Listening)
                return true;
            return false;
        }
    }
}
