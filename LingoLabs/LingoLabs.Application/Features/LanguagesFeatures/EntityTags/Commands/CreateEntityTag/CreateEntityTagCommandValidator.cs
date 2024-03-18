using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.EntityTags.Commands.CreateEntityTag
{
    public class CreateEntityTagCommandValidator: AbstractValidator<CreateEntityTagCommand>
    {
        public CreateEntityTagCommandValidator()
        {
            RuleFor(x => x.EntityId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");

            RuleFor(x => x.TagId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .NotEqual(Guid.Empty).WithMessage("{PropertyName} must not be empty.");
        }
    }
}
