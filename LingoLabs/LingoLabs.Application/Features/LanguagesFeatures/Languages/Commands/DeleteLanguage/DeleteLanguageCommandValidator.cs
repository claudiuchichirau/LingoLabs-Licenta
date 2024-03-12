using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommandValidator: AbstractValidator<DeleteLanguageCommand>
    {
        public DeleteLanguageCommandValidator()
        {
            RuleFor(p => p.LanguageId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
