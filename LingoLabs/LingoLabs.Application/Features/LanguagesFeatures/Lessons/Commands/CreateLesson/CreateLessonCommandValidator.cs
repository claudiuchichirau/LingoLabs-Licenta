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
                .MustAsync((p, cancellation) => ValidateLessonTitle(p.LessonTitle, lessonRepository))
                .WithMessage("LessonTitle must be unique.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLanguageCompetence(p.LanguageCompetenceId, _languageCompetenceRepository))
                .WithMessage("LanguageCompetenceType must be one of the following values: Grammar, Reading, Writing.");

            RuleFor(p => p.ChapterId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> ValidateLessonTitle(string lessonTitle, ILessonRepository lessonRepository)
        {

            if (await lessonRepository.ExistsLessonAsync(lessonTitle) == true)
                return false;
            return true;
        }

        private async Task<bool> ValidateLanguageCompetence(Guid languageCompetenceId, ILanguageCompetenceRepository _languageCompetenceRepository)
        {
            var languageCompetence = await _languageCompetenceRepository.FindByIdAsync(languageCompetenceId);
            if (languageCompetence == null)
                return false;

            LanguageCompetenceType languageCompetenceType = await _languageCompetenceRepository.GetLanguageCompetenceTypeAsync(languageCompetenceId);
            if (languageCompetenceType == LanguageCompetenceType.Grammar || languageCompetenceType == LanguageCompetenceType.Reading || languageCompetenceType == LanguageCompetenceType.Writing)
                return true;
            return false;
        }
    }
}
