using FluentValidation;

namespace LingoLabs.Application.Features.LanguagesFeatures.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandValidator: AbstractValidator<DeleteTagCommand>
    {
        public DeleteTagCommandValidator()
        {
            RuleFor(x => x.TagId)
                .NotEmpty().WithMessage("TagId is required")
                .NotNull()
                .NotEqual(System.Guid.Empty).WithMessage("TagId is required");
        }
    }
}
