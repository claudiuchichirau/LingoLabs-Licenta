using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Languages.Commands.DeletePlacementTest
{
    public class DeletePlacementTestCommandValidator: AbstractValidator<DeletePlacementTestCommand>
    {
        public DeletePlacementTestCommandValidator()
        {
            RuleFor(p => p.LanguageId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");
        }
    }
}
