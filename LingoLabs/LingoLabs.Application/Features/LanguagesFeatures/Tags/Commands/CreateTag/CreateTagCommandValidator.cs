using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Tags.Commands.CreateTag
{
    public class CreateTagCommandValidator: AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(p => p.TagContent)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters.");
        }
    }
}
