using FluentValidation;

namespace LingoLabs.Application.Features.EnrollmentsFeatures.ChapterResults.Commands.CreateChapterResult
{
    public class CreateChapterResultCommandValidator: AbstractValidator<CreateChapterResultCommand>
    {
        public CreateChapterResultCommandValidator()
        {
            RuleFor(p => p.ChapterId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.LanguageCompetenceResults)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
