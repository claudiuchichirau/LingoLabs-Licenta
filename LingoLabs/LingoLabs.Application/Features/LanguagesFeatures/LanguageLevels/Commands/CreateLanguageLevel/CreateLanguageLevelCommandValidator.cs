using FluentValidation;
using LingoLabs.Application.Persistence.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageLevels.Commands.CreateLanguageLevel
{
    public class CreateLanguageLevelCommandValidator: AbstractValidator<CreateLanguageLevelCommand>
    {
        private readonly ILanguageLevelRepository repository;
        public CreateLanguageLevelCommandValidator(ILanguageLevelRepository repository)
        {
            this.repository = repository;

            RuleFor(p => p.LanguageLevelName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p)
               .MustAsync((p, cancellation) => ValidateLanguageLevel(p.LanguageLevelName, repository))
               .WithMessage("LanguageLevelName must be unique.");


            RuleFor(p => p.LanguageLevelAlias)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LanguageId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> ValidateLanguageLevel(string name, ILanguageLevelRepository repository)
        {
            if (await repository.ExistLanguageLevelAsync(name))
                return false;
            return true;
        }
    }
}
