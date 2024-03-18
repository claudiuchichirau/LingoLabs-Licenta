using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.DeleteEntityTag
{
    public class DeleteEntityTagCommandValidator: AbstractValidator<DeleteEntityTagCommand>
    {
        public DeleteEntityTagCommandValidator()
        {
            RuleFor(x => x.EntityTagId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
