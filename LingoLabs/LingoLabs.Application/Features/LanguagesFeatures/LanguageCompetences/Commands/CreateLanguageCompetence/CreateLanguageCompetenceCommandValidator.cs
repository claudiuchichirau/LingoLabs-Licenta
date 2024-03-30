using FluentValidation;
using LingoLabs.Application.Persistence.Languages;
using LingoLabs.Domain.Entities.Languages;

namespace LingoLabs.Application.Features.LanguagesFeatures.LanguageCompetences.Commands.CreateLanguageCompetence
{
    public class CreateLanguageCompetenceCommandValidator : AbstractValidator<CreateLanguageCompetenceCommand>
    {
        private readonly ILanguageCompetenceRepository repository;

        public CreateLanguageCompetenceCommandValidator(ILanguageCompetenceRepository repository)
        {
            this.repository = repository;

            RuleFor(p => p.LanguageCompetenceName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p)
                .MustAsync((p, cancellation) => ValidateLanguageCompetence(p.LanguageCompetenceName, p.LanguageCompetenceType, p.LanguageId))
                .WithMessage("{PropertyName} must have one of the following values: Grammar, Listening, Reading, Writing and must be the same as LanguageCompetenceName");

            RuleFor(p => p.LanguageId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(default(System.Guid)).WithMessage("{PropertyName} is required.");
        }

        private async Task<bool> ValidateLanguageCompetence(string name, LanguageCompetenceType type, Guid languageId)
        {
            if (await repository.ExistsLanguageCompetenceAsync(type, languageId))
                return false;

            if (name == "Listening" && type != LanguageCompetenceType.Listening)
                return false;
            if (name == "Grammar" && type != LanguageCompetenceType.Grammar)
                return false;
            if (name == "Reading" && type != LanguageCompetenceType.Reading)
                return false;
            if (name == "Writing" && type != LanguageCompetenceType.Writing)
                return false;

            return true;
        }
    }
}
